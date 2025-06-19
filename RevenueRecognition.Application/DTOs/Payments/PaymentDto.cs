namespace RevenueRecognition.Application.DTOs.Payments;

public class PaymentDto
{
    public long     Id                { get; set; }
    public long     UpfrontContractId { get; set; }
    public decimal  Amount            { get; set; }
    public DateTime PaidAt            { get; set; }
}