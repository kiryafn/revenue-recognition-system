using RevenueRecognition.Domain.Models;

namespace TripApp.Application.DTOs.Discounts;

public class DiscountUpdateDto
{
    public string? Name           { get; set; }
    public DiscountType?AppliesTo { get; set; }
    public decimal? Percentage    { get; set; }
    public DateTime? StartDate    { get; set; }
    public DateTime? EndDate      { get; set; }
}