using RevenueRecognition.Application.DTOs.Revenue;

namespace RevenueRecognition.Application.Services.Interfaces;

public interface IRevenueService
{
    Task<RevenueResponseDto> CalculateCurrentAsync(RevenueRequestDto req, CancellationToken ct = default);
    Task<RevenueResponseDto> CalculatePredictedAsync(RevenueRequestDto req, CancellationToken ct = default);
}