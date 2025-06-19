namespace RevenueRecognition.Domain.Models;

public class Discount
{
    public long          Id               { get; set; }
    public string        Name             { get; set; } = null!;
    public DiscountType  AppliesTo        { get; set; }
    public decimal       Percentage       { get; set; }  // от 0 до 100
    public DateTime      StartDate        { get; set; }
    public DateTime      EndDate          { get; set; }

    // Принадлежность к продукту (если нужно глобальные скидки, сделайте SoftwareProductId nullable)
    public long          SoftwareProductId { get; set; }
    public SoftwareProduct SoftwareProduct  { get; set; } = null!;
}