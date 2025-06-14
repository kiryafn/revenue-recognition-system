using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface ICompanyClientRepository
{
    Task<CompanyClient?> GetByIdAsync(long id);
    Task<CompanyClient> AddAsync(CompanyClient client, CancellationToken ct = default);
    Task<CompanyClient?> UpdateAsync(CompanyClient client, CancellationToken ct = default);
}