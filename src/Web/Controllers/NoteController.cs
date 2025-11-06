using Microsoft.AspNetCore.Mvc;
using SpendWise.Core.DTOs;
using SpendWise.Web.Services;

namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        // ✅ GET: api/note
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var notes = await _noteService.GetAllAsync();
            return Ok(notes);
        }

        // ✅ GET: api/note/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await _noteService.GetByIdAsync(id);
            if (note == null)
                return NotFound(new { message = $"No se encontró la nota con ID {id}." });

            return Ok(note);
        }

        // ✅ GET: api/note/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var notes = await _noteService.GetByUserIdAsync(userId);
            return Ok(notes);
        }

        // ✅ POST: api/note
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _noteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // ✅ PUT: api/note/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NoteDto dto)
        {
            var updated = await _noteService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new { message = $"No se pudo actualizar la nota con ID {id}." });

            return NoContent();
        }

        // ✅ DELETE: api/note/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _noteService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = $"No se encontró la nota con ID {id}." });

            return NoContent();
        }
    }
}
