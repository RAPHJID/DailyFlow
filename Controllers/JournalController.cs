using JournalApi.Models;
using JournalApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JournalApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _service;

        public JournalController(IJournalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<JournalEntry>>> GetEntries()
        {
            var entries = await _service.GetAllAsync();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<JournalEntry>> GetEntry(int id)
        {
            var entry = await _service.GetByIdAsync(id);
            if (entry == null) return NotFound();
            return Ok(entry);
        }

        [HttpPost]
        public async Task<ActionResult<JournalEntry>> CreateEntry(JournalEntry entry)
        {
            var newEntry = await _service.CreateAsync(entry);
            return CreatedAtAction(nameof(GetEntry), new { id = newEntry.Id }, newEntry);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntry(int id, JournalEntry entry)
        {
            if (id != entry.Id) return BadRequest();

            var updated = await _service.UpdateAsync(entry);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntry(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
