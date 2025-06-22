using RevenueRecognition.Application.DTOs.Contracts.Upfront;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface IUpfrontContractService
{
    Task<UpfrontContractResponseDto> CreateAsync(UpfrontContractCreateDto dto, CancellationToken ct = default);
    Task<UpfrontContractResponseDto?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}