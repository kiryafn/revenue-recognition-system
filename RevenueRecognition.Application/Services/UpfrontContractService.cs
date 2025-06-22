using RevenueRecognition.Application.DTOs.Contracts.Upfront;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Mappers;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using RevenueRecognition.Domain.Models;
using RevenueRecognition.Domain.Models.Enums;

namespace RevenueRecognition.Application.Services;

public class UpfrontContractService(
    IUpfrontContractRepository contracts,
    ISoftwareProductRepository products,
    IDiscountRepository discounts,
    IClientRepository clients)
    : IUpfrontContractService
{
    private const decimal LoyaltyDiscountPct = 5m;
    private const int SupportYearCost = 1000;

    public async Task<UpfrontContractResponseDto> CreateAsync(
        UpfrontContractCreateDto dto,
        CancellationToken ct = default)
    {
        var client = await clients.GetByIdAsync(dto.ClientId, ct)
                     ?? throw new BaseExceptions.NotFoundException($"Client #{dto.ClientId} not found.");

        if (client is IndividualClient { IsDeleted: true })
            throw new InvalidOperationException("Cannot create contract for deleted individual client.");

        var minEnd = dto.StartDate.AddDays(3);
        var maxEnd = dto.StartDate.AddDays(30);
        if (dto.EndDate < minEnd || dto.EndDate > maxEnd)
            throw new ArgumentException("EndDate must be 3–30 days after StartDate.");

        // 4) Валидация supportYears
        if (dto.SupportYears is < 1 or > 4)
            throw new ArgumentException("SupportYears must be between 1 and 4.");

        // 5) Продукт
        var prod = await products.GetByIdAsync(dto.SoftwareProductId, ct)
                   ?? throw new BaseExceptions.NotFoundException("Product not found.");
        if (prod.UpfrontCost == null)
            throw new InvalidOperationException("This product is not sold upfront.");

        // 6) Нет ли у клиента уже активного контракта
        var hasActive = await contracts.HasActiveContractAsync(dto.ClientId, dto.SoftwareProductId, ct);
        if (hasActive)
            throw new InvalidOperationException("Client already has an active contract for this product.");

        // 7) Вычисляем скидку
        var now = DateTime.UtcNow;
        var ds = (await discounts.GetAllByProductIdAsync(dto.SoftwareProductId, ct))
            .Where(d => d.StartDate <= now && d.EndDate >= now)
            .Select(d => d.Percentage);
        var bestDiscount = ds.Any() ? ds.Max() : 0m;

        // 8) Лояльность
        var hasPrev = await contracts.HasSignedContractAsync(dto.ClientId, ct);
        if (hasPrev) bestDiscount += LoyaltyDiscountPct;

        // 9) Расчёт итоговой стоимости
        var baseCost = prod.UpfrontCost.Value;
        var supportExtra = (dto.SupportYears - 1) * SupportYearCost;
        var gross = baseCost + supportExtra;
        var totalCost = Math.Round(gross * (1 - bestDiscount / 100), 2);

        // 10) Создаём сущность
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
        var c = await contracts.GetByIdAsync(id, ct)
                ?? throw new BaseExceptions.NotFoundException("Contract not found.");
        return UpfrontContractMapper.ToDto(c);
    }

    public Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        => contracts.DeleteAsync(id, ct);
}