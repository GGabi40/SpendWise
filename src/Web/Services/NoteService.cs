using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace SpendWise.Web.Services
{
    public class NoteService
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUserService _currentUser;


        public NoteService(INoteRepository noteRepository, IUserRepository userRepository, ICurrentUserService currentUser)
        {
            _noteRepository = noteRepository;
            _userRepository = userRepository;
            _currentUser = currentUser;
        }

        public async Task<Note> AddNoteAsync(NoteDto dto)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            var note = new Note(userId, dto.Title, dto.Content, dto.IsPinned);

            await _noteRepository.AddAsync(note);
            return note;
        }

        //  Obtener todas las notas
        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var notes = await _noteRepository.GetAllAsync();

            // Mapeo de entidad → DTO
            return notes.Select(n => new NoteDto
            {
                // Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsPinned = n.IsPinned
            });
        }

        //  Obtener una nota por ID
        public async Task<NoteDto?> GetByIdAsync(int id)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null) return null;

            return new NoteDto
            {
                Title = note.Title,
                Content = note.Content,
                IsPinned = note.IsPinned
            };
        }

        //  Obtener notas por usuario
        public async Task<IEnumerable<NoteDto>> GetByUserIdAsync()
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var notes = await _noteRepository.GetByUserIdAsync(userId);

            return notes.Select(n => new NoteDto
            {
                // Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                IsPinned = n.IsPinned
            });
        }


        // Actualizar una nota existente
        public async Task<bool> UpdateAsync(int id, NoteDto dto)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var existing = await _noteRepository.GetByIdAsync(id);
            if (existing == null || existing.UserId != userId) return false;

            existing.Title = dto.Title;
            existing.Content = dto.Content;

            await _noteRepository.UpdateAsync(existing);
            return true;
        }

        // Eliminar una nota
        public async Task<bool> DeleteAsync(int id)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var existing = await _noteRepository.GetByIdAsync(id);
            if (existing == null || existing.UserId != userId) return false;

            await _noteRepository.DeleteAsync(id);
            return true;
        }

        // Fijar una nota
        public async Task<bool> PinNoteAsync(int id)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null || note.UserId != userId)
                return false;

            note.Pin();
            await _noteRepository.UpdateAsync(note);
            return true;
        }

        // Desfijar una nota
        public async Task<bool> UnpinNoteAsync(int id)
        {
            var userId = _currentUser.UserId ?? throw new Exception("Usuario no autenticado.");
            var note = await _noteRepository.GetByIdAsync(id);
            if (note == null || note.UserId != userId)
                return false;

            note.Unpin();
            await _noteRepository.UpdateAsync(note);
            return true;
        }
    }
}
