using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.Services.Interfaces;
using TripApp.Application.DTOs.SoftwareProducts;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Authorize]   
[Route("api/software-products")]
public class SoftwareProductsController(ISoftwareProductService svc) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct) =>
        Ok(await svc.GetAllAsync(ct));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(long id, CancellationToken ct) =>
        (await svc.GetByIdAsync(id, ct)) is { } dto
            ? Ok(dto)
            : NotFound();

    [HttpPost]
    public async Task<IActionResult> Create(
        [FromBody] SoftwareProductCreateDto dto,
        CancellationToken ct)
    {
        var created = await svc.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(Get),
            new { id = created.Id }, created);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] SoftwareProductUpdateDto dto,
        CancellationToken ct)
    {
        var updated = await svc.UpdateAsync(id, dto, ct);
        return updated is null
            ? NotFound()
            : Ok(updated);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        long id,
        CancellationToken ct) =>
        await svc.DeleteAsync(id, ct)
            ? NoContent()
            : NotFound();
}