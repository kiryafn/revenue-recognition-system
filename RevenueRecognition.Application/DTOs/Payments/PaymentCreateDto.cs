namespace RevenueRecognition.Application.DTOs.Payments;

public class PaymentCreateDto
{
    public decimal  Amount{ get; set; }
    public DateTime PaidAt{ get; set; }
}