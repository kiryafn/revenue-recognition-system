namespace RevenueRecognition.Domain.Models;

public class Payment
{
    public long        Id                { get; set; }
    
    public long        UpfrontContractId { get; set; }
    public UpfrontContract UpfrontContract { get; set; } = null!;
    
    public decimal     Amount            { get; set; }
    public DateTime    PaidAt            { get; set; }
    
}