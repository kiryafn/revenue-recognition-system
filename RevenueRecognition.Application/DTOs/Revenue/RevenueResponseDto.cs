namespace RevenueRecognition.Application.DTOs.Revenue;

public class RevenueResponseDto
{
    public decimal Amount      { get; set; }
    public string Currency     { get; set; } = null!;
}