using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class IndividualClientRepository(AppDbContext db) : IIndividualClientRepository
{
    public async Task<IndividualClient?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await db.IndividualClients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted, ct);

    public async Task<ICollection<IndividualClient>> GetAllAsync(CancellationToken ct = default) =>
        await db.IndividualClients
            .AsNoTracking()
            .Where(c => !c.IsDeleted)
            .OrderBy(c => c.FirstName)
            .ThenBy(c => c.LastName)
            .ToListAsync(ct);

    public async Task<IndividualClient> AddAsync(IndividualClient client, CancellationToken ct = default)
    {
        db.IndividualClients.Add(client);
        await db.SaveChangesAsync(ct);
        return client;
    }

    public async Task<IndividualClient> UpdateAsync(IndividualClient client, CancellationToken ct = default)
    {
        db.IndividualClients.Update(client);
        await db.SaveChangesAsync(ct);
        return client;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
        var c = await db.IndividualClients.FindAsync([id], ct);
        if (c == null) return false;
        c.IsDeleted = true;
        await db.SaveChangesAsync(ct);
        return true;
    }
}