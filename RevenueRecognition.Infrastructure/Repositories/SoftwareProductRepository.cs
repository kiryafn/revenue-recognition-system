using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class SoftwareProductRepository(AppDbContext db) : ISoftwareProductRepository
{
    public async Task<SoftwareProduct> AddAsync(SoftwareProduct product, CancellationToken ct = default)
    {
        db.SoftwareProducts.Add(product);
        await db.SaveChangesAsync(ct);
        return product;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
        var p = await db.SoftwareProducts.FindAsync(id, ct);
        if (p is null) return false;
        db.SoftwareProducts.Remove(p);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<ICollection<SoftwareProduct>> GetAllAsync(CancellationToken ct = default) =>
        await db.SoftwareProducts.ToListAsync(ct);

    public async Task<SoftwareProduct?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await db.SoftwareProducts.FindAsync([id], ct);

    public async Task<SoftwareProduct> UpdateAsync(SoftwareProduct product, CancellationToken ct = default)
    {
        db.SoftwareProducts.Update(product);
        await db.SaveChangesAsync(ct);
        return product;
    }
}