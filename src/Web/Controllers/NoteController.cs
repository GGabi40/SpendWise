using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Core.DTOs;
using SpendWise.Web.Services;
using System.Security.Claims;

namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly NoteService _noteService;

        public NoteController(NoteService noteService)
        {
            _noteService = noteService;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserNotes()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                            User.FindFirst("id") ??
                            User.FindFirst("userId");

            if (userIdClaim == null)
                return Unauthorized("No se pudo determinar el usuario logueado.");

            int userId = int.Parse(userIdClaim.Value);

            var notes = await _noteService.GetByUserIdAsync(userId);

            if (notes == null || !notes.Any())
                return NotFound("No se encontraron notas para el usuario logueado");

            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var note = await _noteService.GetByIdAsync(id);
            if (note == null)
                return NotFound(new { message = $"No se encontró la nota con ID {id}." });

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NoteDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                            User.FindFirst("id") ??
                            User.FindFirst("userId");

            if (userIdClaim == null)
                return Unauthorized("No se pudo determinar el usuario logueado");

            dto.UserId = int.Parse(userIdClaim.Value);

            var createdNote = await _noteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdNote.Id }, createdNote);
        }


        // VIEJO!
        // [HttpPut("{id}")]
        // public async Task<IActionResult> Update(int id, [FromBody] NoteDto dto)
        // {
        //     var existingNote = await _noteService.GetByIdAsync(id);

        //     if (existingNote == null)
        //         return NotFound($"No se encontró la nota con ID {id}.");

        //     await _noteService.UpdateAsync(id, dto);
        //     return NoContent();
        // }


        // Ahora el userId viene del token, no del JSON y evitas que un usuario cree notas para otro usuario (Probarlo)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NoteDto dto)
        {
            var existingNote = await _noteService.GetByIdAsync(id);

            if (existingNote == null)
                return NotFound($"No se encontró la nota con ID {id}.");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                    User.FindFirst("sub") ??
                    User.FindFirst("id") ??
                    User.FindFirst("userId");

            if (userIdClaim == null)
                return Unauthorized("No se pudo determinar el usuario logueado.");

            dto.UserId = int.Parse(userIdClaim.Value);

            await _noteService.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingNote = await _noteService.GetByIdAsync(id);

            if (existingNote == null)
                return NotFound($"No se encontró la nota con ID {id}.");

            await _noteService.DeleteAsync(id);
            return NoContent();
        }
    }
}
