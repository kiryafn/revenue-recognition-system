using RevenueRecognition.Application.DTOs.Payments;
using RevenueRecognition.Application.Repositories;
using RevenueRecognition.Application.Services.Interfaces;
using RevenueRecognition.Domain.Models;
using RevenueRecognition.Domain.Models.Enums;

namespace RevenueRecognition.Application.Services;

public class PaymentService(
    IUpfrontContractRepository contracts,
    IPaymentRepository payments)
    : IPaymentService
{
    public async Task<PaymentDto> IssuePaymentAsync(long contractId, PaymentCreateDto dto, CancellationToken ct = default)
    {
        var contract = await contracts.GetByIdAsync(contractId, ct)
                       ?? throw new InvalidOperationException("Contract not found.");

        if (dto.PaidAt < contract.StartDate || dto.PaidAt > contract.EndDate)
            throw new InvalidOperationException("Payment date outside contract range.");

        var existing = await payments.GetByContractIdAsync(contractId, ct);
        var paidSum = existing.Sum(p => p.Amount);
        if (paidSum + dto.Amount > contract.TotalCost)
            throw new InvalidOperationException("Payment exceeds total cost.");

        var payment = new Payment
        {
            UpfrontContractId = contractId,
            Amount            = dto.Amount,
            PaidAt            = dto.PaidAt
        };

        var saved = await payments.AddAsync(payment, ct);

        if (Math.Abs((paidSum + dto.Amount) - contract.TotalCost) < 0.01m)
        {
            contract.Status = ContractStatus.Signed;
            await contracts.UpdateAsync(contract, ct);
        }

        return new PaymentDto
        {
            Id                = saved.Id,
            UpfrontContractId = saved.UpfrontContractId,
            Amount            = saved.Amount,
            PaidAt            = saved.PaidAt
        };
    }
}