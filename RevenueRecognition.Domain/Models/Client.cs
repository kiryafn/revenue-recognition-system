namespace RevenueRecognition.Domain.Models;

public abstract class Client
{
    public long Id { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public ClientAddress Address { get; set; } = null!;
    public bool IsDeleted   { get; set; }

}