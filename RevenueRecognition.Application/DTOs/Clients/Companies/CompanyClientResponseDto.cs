namespace RevenueRecognition.Application.DTOs.Companies;

public class CompanyClientResponseDto
{
    public long Id            { get; set; }
    public string CompanyName { get; set; } = null!;
    public string Email       { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string KrsNumber   { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;

}