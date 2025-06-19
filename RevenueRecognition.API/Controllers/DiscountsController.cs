using Microsoft.AspNetCore.Mvc;
using RevenueRecognition.Application.Services.Interfaces;
using TripApp.Application.DTOs.Discounts;

namespace RevenueRecognition.API.Controllers;

[ApiController]
[Route("api/software-products/{productId:long}/discounts")]
public class DiscountsController(IDiscountService svc) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(
        long productId, CancellationToken ct) =>
        Ok(await svc.GetAllByProductAsync(productId, ct));

    [HttpGet("{id:long}")]
    public async Task<IActionResult> Get(
        long productId,
        long id,
        CancellationToken ct) =>
        (await svc.GetByIdAsync(id, ct)) is { } dto && dto.SoftwareProductId == productId
            ? Ok(dto)
            : NotFound();

    [HttpPost]
    public async Task<IActionResult> Create(
        long productId,
        [FromBody] DiscountCreateDto dto,
        CancellationToken ct)
    {
        var created = await svc.CreateAsync(productId, dto, ct);
        return CreatedAtAction(nameof(Get),
            new { productId, id = created.Id }, created);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Update(
        long productId,
        long id,
        [FromBody] DiscountUpdateDto dto,
        CancellationToken ct)
    {
        var updated = await svc.UpdateAsync(id, dto, ct);
        return updated is null || updated.SoftwareProductId != productId
            ? NotFound()
            : Ok(updated);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(
        long productId,
        long id,
        CancellationToken ct) =>
        await svc.DeleteAsync(id, ct)
            ? NoContent()
            : NotFound();
}