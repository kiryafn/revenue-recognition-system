using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class CompanyClientRepository(AppDbContext db) : ICompanyClientRepository
{
    public async Task<CompanyClient?> GetByIdAsync(long id) =>
        await db.CompanyClients.FindAsync(id);

    public async Task<CompanyClient> AddAsync(CompanyClient client, CancellationToken ct = default)
    {
        db.CompanyClients.Add(client);
        await db.SaveChangesAsync(ct);
        return client;
    }

    public async Task<CompanyClient?> UpdateAsync(CompanyClient client, CancellationToken ct = default)
    {
        db.CompanyClients.Update(client);
        await db.SaveChangesAsync(ct);
        return client;
    }
}