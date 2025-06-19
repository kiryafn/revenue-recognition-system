namespace TripApp.Application.DTOs.SoftwareProducts;

public class SoftwareProductUpdateDto
{
    public string? Description       { get; set; }
    public string? Version           { get; set; }
    public string? Category          { get; set; }
    public decimal? UpfrontCost      { get; set; }
    public decimal? SubscriptionCost { get; set; }
}