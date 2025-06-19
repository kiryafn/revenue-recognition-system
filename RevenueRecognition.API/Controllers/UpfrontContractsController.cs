using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.DTOs.Contracts.Upfront;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Route("api/upfront-contracts")]
public class UpfrontContractsController(IUpfrontContractService svc) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] UpfrontContractCreateDto dto,
        CancellationToken ct)
    {
        var created = await svc.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById),
            new { id = created.Id }, created);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetById(long id, CancellationToken ct)
    {
        try
        {
            var e = await svc.GetByIdAsync(id, ct);
            return Ok(e);
        }
        catch (BaseExceptions.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    
    }
        

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct) =>
        await svc.DeleteAsync(id, ct)
            ? NoContent()
            : NotFound();
}