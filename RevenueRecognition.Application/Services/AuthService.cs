using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RevenueRecognition.Application.DTOs.Auth;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Services;

public class AuthService(
    IUserRepository repo,
    IConfiguration configuration) : IAuthService
{
    private readonly string _jwtKey = configuration["Jwt:Key"]!;
    private readonly string _jwtIssuer = configuration["Jwt:Issuer"]!;

    public async Task<string> AuthenticateAsync(LoginDto dto)
    {
        var emp = await repo.GetByLoginAsync(dto.Login);
        if (emp == null) throw new UnauthorizedAccessException("Invalid login");
        if (!BCrypt.Net.BCrypt.Verify(dto.Password, emp.PasswordHash))
            throw new UnauthorizedAccessException("Invalid password");

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, dto.Login),
            new Claim(ClaimTypes.Role, emp.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtIssuer,
            audience: null,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public async Task RegisterAsync(RegisterDto dto, CancellationToken ct = default)
    {
        if (await repo.GetByLoginAsync(dto.Login, ct) is not null)
            throw new InvalidOperationException("Login already exists.");

        var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

        var user = new User
        {
            Login        = dto.Login,
            PasswordHash = hash,
            Role         = dto.Role
        };

        await repo.AddAsync(user, ct);
    }

}