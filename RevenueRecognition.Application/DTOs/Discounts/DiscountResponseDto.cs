using RevenueRecognition.Domain.Models;
using RevenueRecognition.Domain.Models.Enums;

namespace TripApp.Application.DTOs.Discounts;

public class DiscountResponseDto
{
    public long         Id               { get; set; }
    public string       Name             { get; set; } = null!;
    public DiscountType AppliesTo        { get; set; }
    public decimal      Percentage       { get; set; }
    public DateTime     StartDate        { get; set; }
    public DateTime     EndDate          { get; set; }
    public long         SoftwareProductId{ get; set; }
}