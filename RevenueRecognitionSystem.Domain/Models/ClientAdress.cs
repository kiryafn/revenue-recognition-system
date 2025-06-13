namespace RevenueRecognitionSystem.Domain.Models;

public class ClientAddress
{
    public string Street     { get; set; } = null!;
    public string City       { get; set; } = null!;
    public string State      { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public string Country    { get; set; } = null!;
}