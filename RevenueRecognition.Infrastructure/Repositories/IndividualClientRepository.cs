using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class IndividualClientRepository(AppDbContext db) : IIndividualClientRepository
{
    public async Task<IndividualClient?> GetByIdAsync(long id) =>
        await db.IndividualClients.FindAsync(id);

    public async Task<IndividualClient> AddAsync(IndividualClient client, CancellationToken ct = default)
    {
        db.IndividualClients.Add(client);
        await db.SaveChangesAsync(ct);
        return client;
    }

    public async Task<IndividualClient?> UpdateAsync(IndividualClient client, CancellationToken ct = default)
    {
        db.IndividualClients.Update(client);
        await db.SaveChangesAsync(ct);
        return client;
    }
}