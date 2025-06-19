namespace TripApp.Application.DTOs.SoftwareProducts;

public class SoftwareProductResponseDto
{
    public long    Id                { get; set; }
    public string  Name              { get; set; } = null!;
    public string? Description       { get; set; }
    public string  Version           { get; set; } = null!;
    public string  Category          { get; set; } = null!;
    public decimal? UpfrontCost      { get; set; }
    public decimal? SubscriptionCost { get; set; }
}