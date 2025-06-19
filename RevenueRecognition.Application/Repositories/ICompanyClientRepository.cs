using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface ICompanyClientRepository
{
    Task<CompanyClient?> GetByIdAsync(long id, CancellationToken ct = default);
    Task<CompanyClient> AddAsync(CompanyClient client, CancellationToken ct = default);
    Task<CompanyClient?> UpdateAsync(CompanyClient client, CancellationToken ct = default);
    Task<ICollection<CompanyClient>> GetAllAsync(CancellationToken ct = default);
}