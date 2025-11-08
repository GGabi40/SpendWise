using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Core.DTOs;
using SpendWise.Web.Models.Requests;
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
        private readonly UserService _userService;

        public NoteController(NoteService noteService, UserService userService)
        {
            _noteService = noteService;
            _userService = userService;
        }

        [HttpGet("user")]
        public async Task<IActionResult> GetUserNotes()
        {
            var notes = await _noteService.GetByUserIdAsync();

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
        public async Task<IActionResult> AddNote([FromBody] NoteDto dto)
        {
            try
            {
                var note = await _noteService.AddNoteAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = note.Id }, note);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // Ahora el userId viene del token, no del JSON y evitas que un usuario cree notas para otro usuario (Probarlo)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NoteDto dto)
        {
            var existingNote = await _noteService.GetByIdAsync(id);

            if (existingNote == null)
                return NotFound($"No se encontró la nota con ID {id}.");

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
