namespace RevenueRecognition.Domain.Models;

public class SoftwareProduct
{
    public long    Id                 { get; set; }
    public string  Name               { get; set; } = null!;
    public string? Description        { get; set; }
    public string  Version            { get; set; } = null!;
    public string  Category           { get; set; } = null!;
        
    public decimal? UpfrontCost       { get; set; }    
    public decimal? SubscriptionCost  { get; set; } 

    public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
    public ICollection<UpfrontContract> UpfrontContracts { get; set; } = new List<UpfrontContract>();
}