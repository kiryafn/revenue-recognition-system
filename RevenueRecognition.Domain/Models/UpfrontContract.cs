using RevenueRecognition.Domain.Models.Enums;

namespace RevenueRecognition.Domain.Models;

public class UpfrontContract
{
    public long    Id                 { get; set; }
    public long    ClientId           { get; set; }
    public Client  Client             { get; set; } = null!;  

    public long    SoftwareProductId  { get; set; }
    public SoftwareProduct SoftwareProduct { get; set; } = null!;
    
    public string  SoftwareVersion    { get; set; } = null!;

    public DateTime StartDate         { get; set; }
    public DateTime EndDate           { get; set; }

    public decimal BaseCost           { get; set; }
    public int     SupportYears       { get; set; }

    public decimal AppliedDiscountPct { get; set; }
    public decimal TotalCost          { get; set; }

    public ContractStatus Status      { get; set; }

    public ICollection<Payment> Payments     { get; set; } = new List<Payment>();
}