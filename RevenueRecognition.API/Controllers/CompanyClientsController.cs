using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.DTOs.Companies;
using RevenueRecognition.Application.Exceptions;
using RevenueRecognition.Application.Services.Interfaces;

namespace RevenueRecognition.API.Controllers;

[Controller]
[Authorize]  
[Route("api/company-clients")]
public class CompanyClientsController(ICompanyClientService svc) : ControllerBase
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
    public async Task<IActionResult> Create([FromBody] CompanyClientCreateDto dto, CancellationToken ct)
    {
        var created = await svc.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:long}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(long id, [FromBody] CompanyClientUpdateDto dto, CancellationToken ct)
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
}