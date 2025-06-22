using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface ISoftwareProductRepository
{
    Task<SoftwareProduct?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<ICollection<SoftwareProduct>> GetAllAsync(CancellationToken ct = default);
    Task<SoftwareProduct> AddAsync(SoftwareProduct product, CancellationToken ct = default);
    Task<SoftwareProduct> UpdateAsync(SoftwareProduct product, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}