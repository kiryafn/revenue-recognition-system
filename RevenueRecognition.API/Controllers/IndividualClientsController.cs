using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.DTOs.Clients.Individuals;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Authorize]   
[Route("api/individual-clients")]
public class IndividualClientsController(IIndividualClientService svc) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await svc.GetAllAsync());

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id)
    {
        try
        {
            var c = await svc.GetByIdAsync(id);
            return Ok(c);
        }
        catch (BaseExceptions.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IndividualClientCreateDto dto, CancellationToken ct)
    {
        var created = await svc.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(long id, [FromBody] IndividualClientUpdateDto dto, CancellationToken ct)
    {
        try
        {
            var updated = await svc.UpdateAsync(id, dto, ct);
            return Ok(updated);
        }
        catch (BaseExceptions.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("{id:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(long id, CancellationToken ct)
    {
        try
        {
            await svc.DeleteAsync(id, ct);
            return NoContent();
        }
        catch (BaseExceptions.NotFoundException e)
        {
            return NotFound(e.Message);
        }
    }
         
}