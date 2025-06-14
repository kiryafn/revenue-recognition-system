using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Repositories;

public interface IIndividualClientRepository
{
    Task<IndividualClient?> GetByIdAsync(long id);
    Task<IndividualClient> AddAsync(IndividualClient client, CancellationToken ct = default);
    Task<IndividualClient?> UpdateAsync(IndividualClient client, CancellationToken ct = default);
    // и т. д.
}