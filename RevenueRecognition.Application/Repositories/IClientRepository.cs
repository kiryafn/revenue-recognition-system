using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface IClientRepository
{
    Task<Client?> GetByIdAsync(long id, CancellationToken ct = default);
}