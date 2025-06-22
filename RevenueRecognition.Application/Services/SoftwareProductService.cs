using RevenueRecognition.Application.Mappers;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using TripApp.Application.DTOs.SoftwareProducts;

namespace RevenueRecognition.Application.Services;


public class SoftwareProductService(ISoftwareProductRepository repo) : ISoftwareProductService
{
    public async Task<SoftwareProductResponseDto> CreateAsync(SoftwareProductCreateDto dto, CancellationToken ct = default)
    {
        var ent   = SoftwareProductMapper.ToEntity(dto);
        var saved = await repo.AddAsync(ent, ct);
        return SoftwareProductMapper.ToDto(saved);
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default) =>
        await repo.DeleteAsync(id, ct);

    public async Task<IEnumerable<SoftwareProductResponseDto>> GetAllAsync(CancellationToken ct = default) =>
        (await repo.GetAllAsync(ct)).Select(SoftwareProductMapper.ToDto);

    public async Task<SoftwareProductResponseDto?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        var ent = await repo.GetByIdAsync(id, ct);
        return ent is null ? null : SoftwareProductMapper.ToDto(ent);
    }

    public async Task<SoftwareProductResponseDto?> UpdateAsync(long id, SoftwareProductUpdateDto dto, CancellationToken ct = default)
    {
        var ent = await repo.GetByIdAsync(id, ct);
        if (ent is null) return null;
        SoftwareProductMapper.Map(dto, ent);
        var updated = await repo.UpdateAsync(ent, ct);
        return SoftwareProductMapper.ToDto(updated);
    }
}