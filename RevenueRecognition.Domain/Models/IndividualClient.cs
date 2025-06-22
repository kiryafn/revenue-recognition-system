namespace RevenueRecognition.Domain.Models;

public class IndividualClient : Client
{
    public string   FirstName   { get; set; } = null!;
    public string   LastName    { get; set; } = null!;
    public string   Pesel       { get; set; } = null!;
}