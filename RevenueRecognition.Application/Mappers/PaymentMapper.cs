using RevenueRecognition.Application.DTOs.Payments;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Mappers;

public static class PaymentMapper
{
    public static PaymentDto ToDto(Payment p) => new()
    {
        Id                = p.Id,
        UpfrontContractId = p.UpfrontContractId,
        Amount            = p.Amount,
        PaidAt            = p.PaidAt
    };
    
    public static Payment ToEntity(long contractId, PaymentCreateDto dto) => new()
    {
        UpfrontContractId = contractId,
        Amount            = dto.Amount,
        PaidAt            = dto.PaidAt
    };
}