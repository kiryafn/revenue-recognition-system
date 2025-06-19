using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.DTOs.Revenue;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Route("api/revenue")]
public class RevenueController(IRevenueService svc) : ControllerBase
{
    [HttpGet("current")]
    public async Task<IActionResult> GetCurrent(
        [FromQuery] RevenueRequestDto req,
        CancellationToken ct)
    {
        var res = await svc.CalculateCurrentAsync(req, ct);
        return Ok(res);
    }

    [HttpGet("predicted")]
    public async Task<IActionResult> GetPredicted(
        [FromQuery] RevenueRequestDto req,
        CancellationToken ct)
    {
        var res = await svc.CalculatePredictedAsync(req, ct);
        return Ok(res);
    }
}