using RevenueRecognition.Application.DTOs.Contracts.Upfront;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Mappers;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using RevenueRecognition.Domain.Models;

namespace RevenueRecognition.Application.Services;

public class UpfrontContractService(
    IUpfrontContractRepository contracts,
    ISoftwareProductRepository products,
    IDiscountRepository discounts)
    : IUpfrontContractService
{
    private const decimal LoyaltyDiscountPct = 5m;
    private const int SupportYearCost = 1000;

    public async Task<UpfrontContractResponseDto> CreateAsync(UpfrontContractCreateDto dto,
        CancellationToken ct = default)
    {
        var minEnd = dto.StartDate.AddDays(3);
        var maxEnd = dto.StartDate.AddDays(30);
        if (dto.EndDate < minEnd || dto.EndDate > maxEnd)
            throw new ArgumentException("EndDate must be 3–30 days after StartDate.");

        if (dto.SupportYears is < 1 or > 4)
            throw new ArgumentException("SupportYears must be between 1 and 4.");

        var prod = await products.GetByIdAsync(dto.SoftwareProductId, ct)
                   ?? throw new BaseExceptions.NotFoundException("Product not found.");
        if (prod.UpfrontCost == null)
            throw new InvalidOperationException("This product is not sold upfront.");

        var hasActive = await contracts.HasActiveContractAsync(dto.ClientId, dto.SoftwareProductId, ct);
        if (hasActive) throw new InvalidOperationException("Client already has an active contract for this product.");

        var now = DateTime.UtcNow;
        var ds = (await discounts.GetAllByProductIdAsync(dto.SoftwareProductId, ct))
            .Where(d => d.StartDate <= now && d.EndDate >= now)
            .Select(d => d.Percentage);
        var bestDiscount = ds.Any() ? ds.Max() : 0m;

        var hasPrev = await contracts.HasSignedContractAsync(dto.ClientId, ct);
        if (hasPrev) bestDiscount += LoyaltyDiscountPct;

        var baseCost = prod.UpfrontCost.Value;
        var supportExtra = (dto.SupportYears - 1) * SupportYearCost;
        var gross = baseCost + supportExtra;
        var totalCost = Math.Round(gross * (1 - bestDiscount / 100), 2);

        var entity = new UpfrontContract
        {
            ClientId = dto.ClientId,
            SoftwareProductId = dto.SoftwareProductId,
            SoftwareVersion = dto.SoftwareVersion,
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            BaseCost = baseCost,
            SupportYears = dto.SupportYears,
            AppliedDiscountPct = bestDiscount,
            TotalCost = totalCost,
            Status = ContractStatus.Pending
        };

        var saved = await contracts.AddAsync(entity, ct);
        return UpfrontContractMapper.ToDto(saved);
    }

    public async Task<UpfrontContractResponseDto?> GetByIdAsync(long id, CancellationToken ct = default)
    {
        var c = await contracts.GetByIdAsync(id, ct);
        return c != null ? UpfrontContractMapper.ToDto(c) : throw new BaseExceptions.NotFoundException("Contract not found.");
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken ct = default) =>
        await contracts.DeleteAsync(id, ct);
}