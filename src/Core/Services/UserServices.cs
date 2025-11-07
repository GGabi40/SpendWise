using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace Core.Services;

/* Logica de negocios */
public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly INoteRepository _noteRepository;

    public UserService(IUserRepository userRepository, INoteRepository noteRepository)
    {
        _userRepository = userRepository;
        _noteRepository = noteRepository;
    }

     public async Task<NoteDto> AddNoteToUserAsync(int userId, string title, string content, bool isPinned = false)
        {
            // Obtener el usuario
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("Usuario no encontrado.");

            // Usar el método AddNote de User para crear la nota
            user.AddNote(title, content, isPinned);

            // Guardar cambios en el repositorio de usuarios
            await _userRepository.UpdateAsync(user);

            // Obtener la nota recién creada
            var note = user.Notes.Last();

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

    public async Task<UserDto?> GetUserInfo(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? UserDto.Create(user) : null;
    }

    public async Task<UserDto> UpdateUser(int id, string username, string name, string surname, string email)
    {
        var user = await _userRepository.GetByIdAsync(id)
            ?? throw new Exception("Usuario no encontrado.");

        user.UpdateProfile(username, name, surname, email);

        await _userRepository.UpdateAsync(user);

        return UserDto.Create(user);
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.DeleteAsync(id);
    }
}
