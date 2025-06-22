using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class DiscountRepository(AppDbContext db) : IDiscountRepository
{
    public async Task<Discount> AddAsync(Discount discount, CancellationToken ct = default)
    {
        db.Discounts.Add(discount);
        await db.SaveChangesAsync(ct);
        return discount;
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
    {
        var d = await db.Discounts.FindAsync([id], ct);
        if (d is null) return false;
        db.Discounts.Remove(d);
        await db.SaveChangesAsync(ct);
        return true;
    }

    public async Task<IReadOnlyList<Discount>> GetAllByProductIdAsync(long productId, CancellationToken ct = default) =>
        await db.Discounts
            .AsNoTracking()
            .Where(d => d.SoftwareProductId == productId)
            .ToListAsync(ct);

    public async Task<Discount?> GetByIdAsync(long id, CancellationToken ct = default) =>
        await db.Discounts
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id, ct);

    public async Task<Discount> UpdateAsync(Discount discount, CancellationToken ct = default)
    {
        db.Discounts.Update(discount);
        await db.SaveChangesAsync(ct);
        return discount;
    }
}