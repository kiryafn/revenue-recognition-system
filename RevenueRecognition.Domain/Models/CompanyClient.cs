namespace RevenueRecognition.Domain.Models;

public class CompanyClient : Client
{
    public string CompanyName   { get; set; } = null!;
    public string KrsNumber     { get; set; } = null!;
}