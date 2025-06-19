using RevenueRecognition.Domain.Models;
using TripApp.Application.DTOs.SoftwareProducts;

namespace RevenueRecognition.Application.Mappers;

public static class SoftwareProductMapper
{
    public static SoftwareProductResponseDto ToDto(SoftwareProduct p) =>
        new SoftwareProductResponseDto
        {
            Id               = p.Id,
            Name             = p.Name,
            Description      = p.Description,
            Version          = p.Version,
            Category         = p.Category,
            UpfrontCost      = p.UpfrontCost,
            SubscriptionCost = p.SubscriptionCost
        };

    public static SoftwareProduct ToEntity(SoftwareProductCreateDto dto) =>
        new SoftwareProduct
        {
            Name              = dto.Name,
            Description       = dto.Description,
            Version           = dto.Version,
            Category          = dto.Category,
            UpfrontCost       = dto.UpfrontCost,
            SubscriptionCost  = dto.SubscriptionCost
        };

    public static void Map(SoftwareProductUpdateDto dto, SoftwareProduct p)
    {
        if (dto.Description       is not null) p.Description       = dto.Description;
        if (dto.Version           is not null) p.Version           = dto.Version;
        if (dto.Category          is not null) p.Category          = dto.Category;
        if (dto.UpfrontCost       is not null) p.UpfrontCost       = dto.UpfrontCost;
        if (dto.SubscriptionCost  is not null) p.SubscriptionCost  = dto.SubscriptionCost;
    }
}