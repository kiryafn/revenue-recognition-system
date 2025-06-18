using RevenueRecognition.Application.DTOs.Clients.Individuals;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface IIndividualClientService
{
    Task<IndividualClientResponseDto> CreateAsync(IndividualClientCreateDto dto, CancellationToken ct = default);
    Task<IndividualClientResponseDto?> GetByIdAsync(long id);
    Task<IEnumerable<IndividualClientResponseDto>> GetAllAsync();
    Task<IndividualClientResponseDto?> UpdateAsync(long id, IndividualClientUpdateDto dto, CancellationToken ct = default);
    Task DeleteAsync(long id, CancellationToken ct = default);
}