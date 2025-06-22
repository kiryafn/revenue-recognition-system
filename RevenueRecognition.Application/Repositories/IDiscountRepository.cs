using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface IDiscountRepository
{
    Task<Discount?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<IReadOnlyList<Discount>> GetAllByProductIdAsync(long productId, CancellationToken ct = default);
    Task<Discount> AddAsync(Discount discount, CancellationToken ct = default);
    Task<Discount> UpdateAsync(Discount discount, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}