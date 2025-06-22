namespace RevenueRecognition.Domain.Models;

public class User
{
    public long   Id           { get; set; }
    public string Login        { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role         { get; set; } = null!;
}