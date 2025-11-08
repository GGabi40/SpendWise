using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace SpendWise.Web.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;

        public NoteService(INoteRepository noteRepository, IUserRepository userRepository)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
        }

        public async Task<Note> AddNoteAsync(NoteDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            var note = new Note(dto.UserId, dto.Title, dto.Content);

            await _noteRepository.AddAsync(note);
            return note;
        }

        //  Obtener todas las notas
        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var notes = await _noteRepository.GetAllAsync();

            // Mapeo de entidad → DTO
            return notes.Select(n => new NoteDto
            {
                Title = n.Title,
                Content = n.Content,
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
                Title = note.Title,
                Content = note.Content,
                UserId = note.UserId
            };
        }

        //  Obtener notas por usuario
        public async Task<IEnumerable<NoteDto>> GetByUserIdAsync(int userId)
        {
            var notes = await _noteRepository.GetByUserIdAsync(userId);

            return notes.Select(n => new NoteDto
            {
                Title = n.Title,
                Content = n.Content,
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
