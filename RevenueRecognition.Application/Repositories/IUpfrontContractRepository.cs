using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface IUpfrontContractRepository
{
    Task<UpfrontContract?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<ICollection<UpfrontContract>> GetByClientIdAsync(long clientId, CancellationToken ct = default);
    Task<bool> HasActiveContractAsync(long clientId, long productId, CancellationToken ct = default);
    Task<bool> HasSignedContractAsync(long clientId, CancellationToken ct = default);
    Task<UpfrontContract> AddAsync(UpfrontContract contract, CancellationToken ct = default);
    Task<UpfrontContract> UpdateAsync(UpfrontContract contract, CancellationToken ct = default);
    Task<bool> DeleteAsync(long id, CancellationToken ct = default);
}