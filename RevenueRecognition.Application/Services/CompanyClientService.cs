using RevenueRecognition.Application.DTOs.Companies;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Mappers;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.Application.Services;

public class CompanyClientService(ICompanyClientRepository repo) : ICompanyClientService
{
    public async Task<CompanyClientResponseDto> CreateAsync(CompanyClientCreateDto dto, CancellationToken ct = default)
    {
        var entity = CompanyClientMapper.ToEntity(dto);
        var saved  = await repo.AddAsync(entity, ct);
        return CompanyClientMapper.ToDto(saved);
    }
    
    public async Task<IEnumerable<CompanyClientResponseDto>> GetAllAsync()
    {
        var list = await repo.GetAllAsync();
        return list.Select(CompanyClientMapper.ToDto);
    }

    public async Task<CompanyClientResponseDto?> GetByIdAsync(long id)
    {
        var c = await repo.GetByIdAsync(id);
        return c is not null ? CompanyClientMapper.ToDto(c) : throw new BaseExceptions.NotFoundException("Company client not found");
    }

    public async Task<CompanyClientResponseDto?> UpdateAsync(long id, CompanyClientUpdateDto dto, CancellationToken ct = default)
    {
        var c = await repo.GetByIdAsync(id);
        if (c is null) throw new BaseExceptions.NotFoundException("Company client not found");
        
        CompanyClientMapper.Map(dto, c);
        var updated = await repo.UpdateAsync(c, ct);
        return CompanyClientMapper.ToDto(updated);
    }

    public async Task DeleteAsync(long id, CancellationToken ct = default)
    {
        var c = await repo.GetByIdAsync(id);
        if (c is null) throw new BaseExceptions.NotFoundException("Company client not found");
        
        c.IsDeleted = true;
        await repo.UpdateAsync(c, ct);
    }
}