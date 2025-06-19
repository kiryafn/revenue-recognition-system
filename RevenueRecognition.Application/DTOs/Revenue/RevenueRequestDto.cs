namespace RevenueRecognition.Application.DTOs.Revenue;

public class RevenueRequestDto
{
    public long? ProductId     { get; set; }
    public string Currency     { get; set; } = "PLN";
}