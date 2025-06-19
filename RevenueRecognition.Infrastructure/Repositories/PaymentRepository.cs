using Microsoft.EntityFrameworkCore;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Infrastructure.Repositories;

public class PaymentRepository(AppDbContext db) : IPaymentRepository
{
    public async Task<Payment> AddAsync(Payment payment, CancellationToken ct = default)
    {
        db.Payments.Add(payment);
        await db.SaveChangesAsync(ct);
        return payment;
    }

    public async Task<ICollection<Payment>> GetByContractIdAsync(long contractId, CancellationToken ct = default) =>
        await db.Payments
            .AsNoTracking()
            .Where(p => p.UpfrontContractId == contractId)
            .ToListAsync(ct);
}