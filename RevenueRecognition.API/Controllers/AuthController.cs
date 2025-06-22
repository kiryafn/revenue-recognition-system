using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.DTOs.Auth;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService svc) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var token = await svc.AuthenticateAsync(dto);
        return Ok(new { token });
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        await svc.RegisterAsync(dto);
        return CreatedAtAction(
            nameof(Login),
            null,
            new { message = "User registered successfully" });
    }
}