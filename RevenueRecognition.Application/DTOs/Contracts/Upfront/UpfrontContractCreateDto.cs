namespace RevenueRecognition.Application.DTOs.Contracts.Upfront;

public class UpfrontContractCreateDto
{
    public long   ClientId        { get; set; }
    public long   SoftwareProductId { get; set; }
    public string SoftwareVersion { get; set; } = null!;
    public DateTime StartDate     { get; set; }
    public DateTime EndDate       { get; set; }
    public int    SupportYears    { get; set; }  
}