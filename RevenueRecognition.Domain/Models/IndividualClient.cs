namespace RevenueRecognition.Domain.Models;

public class IndividualClient
{
    public long Id               { get; set; }
    public string FirstName      { get; set; } = null!;
    public string LastName       { get; set; } = null!;
    public ClientAddress Address { get; set; }
    public string Email          { get; set; } = null!;
    public string PhoneNumber    { get; set; } = null!;
    public string Pesel          { get; set; } = null!;
    public bool IsDeleted        { get; set; }
}