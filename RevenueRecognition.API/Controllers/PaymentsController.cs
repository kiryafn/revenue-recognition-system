using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.DTOs.Payments;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Authorize]   
[Route("api/upfront-contracts/{contractId:long}/payments")]
public class PaymentsController(IPaymentService svc) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Issue(
        long contractId,
        [FromBody] PaymentCreateDto dto,
        CancellationToken ct)
    {
        var payment = await svc.IssuePaymentAsync(contractId, dto, ct);
        return CreatedAtAction(
            null,
            new { contractId, id = payment.Id },
            payment);
    }
}