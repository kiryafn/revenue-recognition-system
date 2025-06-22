namespace RevenueRecognition.Application.DTOs.Auth;

public class RegisterDto
{
    public string Login    { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Role     { get; set; } = "User";
}