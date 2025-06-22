namespace RevenueRecognition.Domain.Models;

public class ClientAddress
{
    public string Country    { get; set; } = null!;
    public string City       { get; set; } = null!;
    public string Street     { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}