using RevenueRecognition.Application.DTOs.Payments;
using RevenueRecognition.Domain.Models;
using RevenueRecognition.Domain.Models.Enums;

namespace RevenueRecognition.Application.DTOs.Contracts.Upfront;

public class UpfrontContractResponseDto
{
    public long    Id                  { get; set; }
    public long    ClientId            { get; set; }
    public long    SoftwareProductId   { get; set; }
    public string  SoftwareVersion     { get; set; } = null!;
    public DateTime StartDate          { get; set; }
    public DateTime EndDate            { get; set; }
    public decimal BaseCost            { get; set; }
    public int     SupportYears        { get; set; }
    public decimal AppliedDiscountPct  { get; set; }
    public decimal TotalCost           { get; set; }
    public ContractStatus Status       { get; set; }
    public ICollection<PaymentDto> Payments { get; set; } = new List<PaymentDto>();
}