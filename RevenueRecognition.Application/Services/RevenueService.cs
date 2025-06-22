using RevenueRecognition.Application.DTOs.Revenue;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using RevenueRecognition.Domain.Models;
using RevenueRecognition.Domain.Models.Enums;

namespace RevenueRecognition.Application.Services;

public class RevenueService(
    IUpfrontContractRepository contracts,
    IPaymentRepository payments,
    IExchangeRateProvider rates)
    : IRevenueService
{
    public async Task<RevenueResponseDto> CalculateCurrentAsync(
            RevenueRequestDto req, CancellationToken ct = default)
        {
            var signed = await contracts
                .GetAllAsync(ct: ct); 

            var filtered = signed.Where(c =>
                c.Status == ContractStatus.Signed &&
                (req.ProductId == null || c.SoftwareProductId == req.ProductId));

            var sumPln = 0m;
            foreach (var c in filtered)
            {
                var pays = await payments.GetByContractIdAsync(c.Id, ct);
                sumPln += pays.Sum(p => p.Amount);
            }

            var rate = await rates.GetRateAsync("PLN", req.Currency, ct);
            var amount = Math.Round(sumPln * rate, 2);

            return new RevenueResponseDto
            {
                Amount   = amount,
                Currency = req.Currency
            };
        }

        public async Task<RevenueResponseDto> CalculatePredictedAsync(
            RevenueRequestDto req, CancellationToken ct = default)
        {
            var current = await CalculateCurrentAsync(req, ct);

            var all = await contracts.GetAllAsync(ct: ct);
            var pending = all.Where(c =>
                c.Status == ContractStatus.Pending &&
                (req.ProductId == null || c.SoftwareProductId == req.ProductId));

            var sumPending = pending.Sum(c => c.TotalCost);

            var rate = await rates.GetRateAsync("PLN", req.Currency, ct);
            var predictedAmount = Math.Round(current.Amount + sumPending * rate, 2);

            return new RevenueResponseDto
            {
                Amount   = predictedAmount,
                Currency = req.Currency
            };
        }
    }