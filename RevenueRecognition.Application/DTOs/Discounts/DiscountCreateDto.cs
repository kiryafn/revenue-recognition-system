using RevenueRecognition.Domain.Models;

namespace TripApp.Application.DTOs.Discounts;

public class DiscountCreateDto
{
    public string Name            { get; set; } = null!;
    public DiscountType AppliesTo { get; set; }
    public decimal Percentage     { get; set; }
    public DateTime StartDate     { get; set; }
    public DateTime EndDate       { get; set; }
}