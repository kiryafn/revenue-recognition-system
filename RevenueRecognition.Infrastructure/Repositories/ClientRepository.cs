using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class ClientRepository(AppDbContext db) : IClientRepository
{
    public async Task<Client?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await db.Clients.FindAsync([id], ct);
}