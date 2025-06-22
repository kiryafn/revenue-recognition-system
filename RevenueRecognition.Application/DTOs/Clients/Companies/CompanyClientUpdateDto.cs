namespace RevenueRecognition.Application.DTOs.Companies;

public class CompanyClientUpdateDto
{
    public string? CompanyName { get; set; }
    public string? Email       { get; set; } 
    public string? PhoneNumber { get; set; } 
    public AddressDto? Address { get; set; }
    public bool? IsDeleted     { get; set; }
}