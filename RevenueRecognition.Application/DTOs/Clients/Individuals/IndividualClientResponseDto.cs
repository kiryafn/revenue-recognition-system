namespace RevenueRecognition.Application.DTOs.Clients.Individuals;

public class IndividualClientResponseDto
{
    public long Id            { get; set; }
    public string FirstName   { get; set; } = null!;
    public string LastName    { get; set; } = null!;
    public string Email       { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Pesel       { get; set; } = null!;
    public AddressDto Address { get; set; } = null!;
}