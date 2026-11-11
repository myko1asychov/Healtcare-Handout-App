using HandoverApi.Data;
using HandoverApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandoverApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HandoversController : ControllerBase
{
    private readonly AppDbContext _context;

    public HandoversController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/handovers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Handover>>> GetHandovers()
    {
        var handovers = _context.Handovers
        
        .AsEnumerable()
        .OrderByDescending(h => h.CreatedAt)
        .ToList();

        return Ok(handovers);
    }

    // GET: api/handovers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Handover>> GetHandover(int id)
    {
        var handover = await _context.Handovers.FindAsync(id);

        if (handover == null)
        {
            return NotFound(new { message = "Übergabe wurde nicht gefunden." });
        }

        return handover;
    }

    // POST: api/handovers
    [HttpPost]
    public async Task<ActionResult<Handover>> PostHandover(Handover handover)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        _context.Handovers.Add(handover);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHandover), new { id = handover.Id }, handover);
    }

    // PUT: api/handovers/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHandover(int id, Handover handover)
    {
        if (id != handover.Id)
        {
            return BadRequest(new { message = "ID stimmt nicht überein." });
        }

        _context.Entry(handover).State = EntityState.Modified;

        // Защита: CreatedAt не должен перезаписываться клиентом
        _context.Entry(handover).Property(x => x.CreatedAt).IsModified = false;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Handovers.Any(e => e.Id == id))
            {
                return NotFound(new { message = "Übergabe wurde inzwischen gelöscht." });
            }
            throw;
        }

        return NoContent();
    }

    // DELETE: api/handovers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHandover(int id)
    {
        var handover = await _context.Handovers.FindAsync(id);
        if (handover == null)
        {
            return NotFound(new { message = "Übergabe wurde nicht gefunden." });
        }

        _context.Handovers.Remove(handover);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}