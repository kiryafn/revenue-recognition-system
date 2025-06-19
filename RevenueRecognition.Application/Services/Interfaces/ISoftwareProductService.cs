using TripApp.Application.DTOs.SoftwareProducts;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface ISoftwareProductService
{
    Task<IEnumerable<SoftwareProductResponseDto>> GetAllAsync(CancellationToken ct = default);
    Task<SoftwareProductResponseDto?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<SoftwareProductResponseDto> CreateAsync(SoftwareProductCreateDto dto, CancellationToken ct = default);
    Task<SoftwareProductResponseDto?> UpdateAsync(long id, SoftwareProductUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}