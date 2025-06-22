using RevenueRecognition.Application.Mappers;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using TripApp.Application.DTOs.Discounts;

namespace RevenueRecognition.Application.Services;

public class DiscountService(IDiscountRepository repo) : IDiscountService
{
    public async Task<DiscountResponseDto> CreateAsync(long productId, DiscountCreateDto dto, CancellationToken ct = default)
    {
        var ent   = DiscountMapper.ToEntity(productId, dto);
        var saved = await repo.AddAsync(ent, ct);
        return DiscountMapper.ToDto(saved);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default) =>
        await repo.DeleteAsync(id, ct);

    public async Task<IEnumerable<DiscountResponseDto>> GetAllByProductAsync(long productId, CancellationToken ct = default) =>
        (await repo.GetAllByProductIdAsync(productId, ct))
        .Select(DiscountMapper.ToDto);

    public async Task<DiscountResponseDto?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        var ent = await repo.GetByIdAsync(id, ct);
        return ent is null ? null : DiscountMapper.ToDto(ent);
    }

    public async Task<DiscountResponseDto?> UpdateAsync(long id, DiscountUpdateDto dto, CancellationToken ct = default)
    {
        var ent = await repo.GetByIdAsync(id, ct);
        if (ent is null) return null;
        DiscountMapper.Map(dto, ent);
        var updated = await repo.UpdateAsync(ent, ct);
        return DiscountMapper.ToDto(updated);
    }
}