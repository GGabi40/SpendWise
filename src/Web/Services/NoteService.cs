using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace SpendWise.Web.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        //  Obtener todas las notas
        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var notes = await _noteRepository.GetAllAsync();

            // Mapeo de entidad → DTO
            return notes.Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsPinned = n.IsPinned,
                CreatedAt = n.CreatedAt,
                UserId = n.UserId
            });
        }

        //  Obtener una nota por ID
        public async Task<NoteDto?> GetByIdAsync(int id)
        {
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null) return null;

            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                IsPinned = note.IsPinned,
                CreatedAt = note.CreatedAt,
                UserId = note.UserId
            };
        }

        //  Obtener notas por usuario
        public async Task<IEnumerable<NoteDto>> GetByUserIdAsync(int userId)
        {
            var notes = await _noteRepository.GetByUserIdAsync(userId);

            return notes.Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsPinned = n.IsPinned,
                CreatedAt = n.CreatedAt,
                UserId = n.UserId
            });
        }


        // Actualizar una nota existente
        public async Task<bool> UpdateAsync(int id, NoteDto dto)
        {
            var existing = await _noteRepository.GetByIdAsync(id);
            if (existing == null) return false;

            existing.Title = dto.Title;
            existing.Content = dto.Content;
            existing.IsPinned = dto.IsPinned;

            await _noteRepository.UpdateAsync(existing);
            return true;
        }

        // Eliminar una nota
        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _noteRepository.GetByIdAsync(id);
            if (existing == null) return false;

            await _noteRepository.DeleteAsync(id);
            return true;
        }
    }
}
