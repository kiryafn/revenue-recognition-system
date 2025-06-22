using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;
using RevenueRecognition.Domain.Models.Enums;

namespace RevenueRecognition.Infrastructure.Repositories;

public class UpfrontContractRepository(AppDbContext db) : IUpfrontContractRepository
{
    public async Task<UpfrontContract?> GetByIdAsync(long id, CancellationToken ct = default) =>
            await db.UpfrontContracts
                     .Include(c => c.Payments)
                     .FirstOrDefaultAsync(c => c.Id == id, ct);

        public async Task<ICollection<UpfrontContract>> GetByClientIdAsync(long clientId, CancellationToken ct = default) =>
            await db.UpfrontContracts
                     .Where(c => c.ClientId == clientId)
                     .ToListAsync(ct);

        public async Task<bool> HasActiveContractAsync(long clientId, long productId, CancellationToken ct = default) =>
            await db.UpfrontContracts
                     .AnyAsync(c =>
                         c.ClientId == clientId &&
                         c.SoftwareProductId == productId &&
                         (c.Status == ContractStatus.Pending || c.Status == ContractStatus.Signed),
                         ct);

        public async Task<bool> HasSignedContractAsync(long clientId, CancellationToken ct = default) =>
            await db.UpfrontContracts
                     .AnyAsync(c =>
                         c.ClientId == clientId &&
                         c.Status == ContractStatus.Signed,
                         ct);

        public async Task<UpfrontContract> AddAsync(UpfrontContract contract, CancellationToken ct = default)
        {
            db.UpfrontContracts.Add(contract);
            await db.SaveChangesAsync(ct);
            return contract;
        }

        public async Task<UpfrontContract> UpdateAsync(UpfrontContract contract, CancellationToken ct = default)
        {
            db.UpfrontContracts.Update(contract);
            await db.SaveChangesAsync(ct);
            return contract;
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {   
            var c = await db.UpfrontContracts.FindAsync([id], ct);
            if (c == null) return false;
            db.UpfrontContracts.Remove(c);
            await db.SaveChangesAsync(ct);
            return true;
        }

        public async Task<ICollection<UpfrontContract>> GetAllAsync(CancellationToken ct = default)
        {
            return await db.UpfrontContracts.ToListAsync(ct);
        }
}