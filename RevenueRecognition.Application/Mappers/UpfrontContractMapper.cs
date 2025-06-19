using RevenueRecognition.Application.DTOs.Contracts.Upfront;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Mappers;

public static class UpfrontContractMapper
{
    public static UpfrontContractResponseDto ToDto(UpfrontContract c) => new()
    {
        Id                 = c.Id,
        ClientId           = c.ClientId,
        SoftwareProductId  = c.SoftwareProductId,
        SoftwareVersion    = c.SoftwareVersion,
        StartDate          = c.StartDate,
        EndDate            = c.EndDate,
        BaseCost           = c.BaseCost,
        SupportYears       = c.SupportYears,
        AppliedDiscountPct = c.AppliedDiscountPct,
        TotalCost          = c.TotalCost,
        Status             = c.Status,
        Payments           = c.Payments
            .Select(PaymentMapper.ToDto)
            .ToList()
    };
}