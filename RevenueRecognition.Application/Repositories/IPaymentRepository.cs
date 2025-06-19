using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface IPaymentRepository
{
    Task<ICollection<Payment>> GetByContractIdAsync(long contractId, CancellationToken ct = default);
    Task<Payment> AddAsync(Payment payment, CancellationToken ct = default);
}