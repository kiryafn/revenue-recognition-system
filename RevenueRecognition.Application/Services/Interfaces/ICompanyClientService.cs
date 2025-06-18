using RevenueRecognition.Application.DTOs.Companies;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface ICompanyClientService
{
    Task<CompanyClientResponseDto> CreateAsync(CompanyClientCreateDto dto, CancellationToken ct = default);
    Task<CompanyClientResponseDto?> GetByIdAsync(long id);
    Task<IEnumerable<CompanyClientResponseDto>> GetAllAsync();
    Task<CompanyClientResponseDto?> UpdateAsync(long id, CompanyClientUpdateDto dto, CancellationToken ct = default);
    Task DeleteAsync(long id, CancellationToken ct = default);
}