using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class IndividualClientRepository(AppDbContext db) : IIndividualClientRepository
{
    public async Task<ICollection<IndividualClient>> GetAllAsync()
    {
        return await db.IndividualClients
            .Where(c => !c.IsDeleted) 
            .OrderBy(c => c.FirstName)
            .ThenBy(c => c.LastName)
            .ToListAsync();
    }

    public async Task<IndividualClient?> GetByIdAsync(long id)
    {
        return await db.IndividualClients
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);    }

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