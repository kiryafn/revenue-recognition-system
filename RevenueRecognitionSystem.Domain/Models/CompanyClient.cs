namespace RevenueRecognitionSystem.Domain.Models;

public class CompanyClient
{
    public long Id             { get; set; }
    public string CompanyName  { get; set; } = null!;
    public string TaxId        { get; set; } = null!;
    public string ContactPerson{ get; set; } = null!;
    public string Email        { get; set; } = null!;
    public string PhoneNumber  { get; set; } = null!;
    public ClientAddress Address { get; set; } = new ClientAddress();
    public DateTime RegisteredAt { get; set; }
}