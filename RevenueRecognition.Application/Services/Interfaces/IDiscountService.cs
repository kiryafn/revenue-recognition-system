using TripApp.Application.DTOs.Discounts;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface IDiscountService
{
    Task<IEnumerable<DiscountResponseDto>> GetAllByProductAsync(long productId, CancellationToken ct = default);
    Task<DiscountResponseDto?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<DiscountResponseDto> CreateAsync(long productId, DiscountCreateDto dto, CancellationToken ct = default);
    Task<DiscountResponseDto?> UpdateAsync(long id, DiscountUpdateDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}