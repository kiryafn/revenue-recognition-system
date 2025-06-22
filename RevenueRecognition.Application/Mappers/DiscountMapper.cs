using RevenueRecognition.Domain.Models;
using TripApp.Application.DTOs.Discounts;

namespace RevenueRecognition.Application.Mappers;

public static class DiscountMapper
{
    public static DiscountResponseDto ToDto(Discount d) =>
        new()
        {
            Id                 = d.Id,
            Name               = d.Name,
            AppliesTo          = d.AppliesTo,
            Percentage         = d.Percentage,
            StartDate          = d.StartDate,
            EndDate            = d.EndDate,
            SoftwareProductId  = d.SoftwareProductId
        };

    public static Discount ToEntity(long productId, DiscountCreateDto dto) =>
        new()
        {
            SoftwareProductId  = productId,
            Name               = dto.Name,
            AppliesTo          = dto.AppliesTo,
            Percentage         = dto.Percentage,
            StartDate          = dto.StartDate,
            EndDate            = dto.EndDate
        };

    public static void Map(DiscountUpdateDto dto, Discount d)
    {
        if (dto.Name        is not null) d.Name        = dto.Name;
        if (dto.AppliesTo   is not null) d.AppliesTo   = dto.AppliesTo.Value;
        if (dto.Percentage  is not null) d.Percentage  = dto.Percentage.Value;
        if (dto.StartDate   is not null) d.StartDate   = dto.StartDate.Value;
        if (dto.EndDate     is not null) d.EndDate     = dto.EndDate.Value;
    }
}