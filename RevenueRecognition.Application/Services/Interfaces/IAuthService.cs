using RevenueRecognition.Application.DTOs.Auth;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface IAuthService
{
    Task<string> AuthenticateAsync(LoginDto dto);
    Task RegisterAsync(RegisterDto dto, CancellationToken ct = default);
}