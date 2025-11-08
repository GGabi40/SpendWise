using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace Core.Services;

/* Logica de negocios */
public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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
