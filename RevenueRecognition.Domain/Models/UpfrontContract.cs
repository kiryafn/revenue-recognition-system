namespace RevenueRecognition.Domain.Models;

public class UpfrontContract
{
    public long               Id              { get; set; }
    public long               ClientId        { get; set; }
    public long               SoftwareProductId { get; set; }
    public string             SoftwareVersion { get; set; } = null!;

    public DateTime           StartDate       { get; set; }
    public DateTime           EndDate         { get; set; }   // must be between Start+3d…Start+30d

    public decimal            BaseCost        { get; set; }   // из SoftwareProduct.UpfrontCost
    public int                SupportYears    { get; set; }   // 1…4 (1 год + 0–3 доп.)

    public decimal            AppliedDiscountPct { get; set; } // итоговый % скидки (включая loyalty)
    public decimal            TotalCost       { get; set; }   // = (BaseCost + (SupportYears-1)*1000) * (1 - AppliedDiscountPct/100)

    public ContractStatus     Status          { get; set; }   // Pending / Signed / Cancelled

    // Навигационные свойства
    public IndividualClient?  IndividualClient { get; set; }
    public CompanyClient?     CompanyClient    { get; set; }
    public SoftwareProduct    SoftwareProduct  { get; set; } = null!;
    public ICollection<Payment> Payments       { get; set; } = new List<Payment>();
}