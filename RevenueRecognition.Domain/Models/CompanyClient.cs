namespace RevenueRecognition.Domain.Models;

public class CompanyClient
{
    public long Id               { get; set; }
    public string CompanyName    { get; set; } = null!;
    public string Email          { get; set; } = null!;
    public string PhoneNumber    { get; set; } = null!;
    public string KrsNumber      { get; set; } = null!;
    public ClientAddress Address { get; set; } = null!;
    public bool IsDeleted        { get; set; }


}