using RevenueRecognition.Application.DTOs.Payments;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface IPaymentService
{
    Task<PaymentDto> IssuePaymentAsync(long contractId, PaymentCreateDto dto, CancellationToken ct = default);

}