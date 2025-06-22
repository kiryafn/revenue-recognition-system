using RevenueRecognition.Application.DTOs.Clients.Individuals;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Mappers;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.Application.Services;


public class IndividualClientService(IIndividualClientRepository repo) : IIndividualClientService
{
    public async Task<IndividualClientResponseDto> CreateAsync(IndividualClientCreateDto dto, CancellationToken ct = default)
    {
        var entity = IndividualClientMapper.ToEntity(dto);
        var saved  = await repo.AddAsync(entity, ct);
        return IndividualClientMapper.ToDto(saved);
    }

    public async Task<IEnumerable<IndividualClientResponseDto>> GetAllAsync(CancellationToken ct = default)
    {
        var list = await repo.GetAllAsync(ct);
        return list.Select(IndividualClientMapper.ToDto);
    }

    public async Task<IndividualClientResponseDto?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        var c = await repo.GetByIdAsync(id, ct)
                ?? throw new BaseExceptions.NotFoundException($"Individual client #{id} not found");
        return IndividualClientMapper.ToDto(c);
    }

    public async Task<IndividualClientResponseDto?> UpdateAsync(long id, IndividualClientUpdateDto dto, CancellationToken ct = default)
    {
        var c = await repo.GetByIdAsync(id, ct)
                ?? throw new BaseExceptions.NotFoundException($"Individual client #{id} not found");
            
        IndividualClientMapper.Map(dto, c);
        var updated = await repo.UpdateAsync(c, ct);
        return IndividualClientMapper.ToDto(updated);
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        var c = await repo.GetByIdAsync(id, ct)
                ?? throw new BaseExceptions.NotFoundException($"Individual client #{id} not found");

        c.IsDeleted = true;
        await repo.UpdateAsync(c, ct);
    }
}