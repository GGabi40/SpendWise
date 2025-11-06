
using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace Core.Services;

/* Logica de negocios */
public class UserServices
{
    private readonly IUserRepository _userRepository;

    public UserServices(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /* Métodos referentes a usuarios */
    public UserDto CreateUser(string Username, string Name, string Surname, string Email, string Password)
    {
        var user = _userRepository.Add(new User
        {
            Username = Username,
            Name = Name,
            Surname = Surname,
            Email = Email,
            Password = Password
        });

        _userRepository.SaveChanges();
        return UserDto.Create(user);
    }

    public UserDto UpdateUser(int id, int idDto, string username, string name, string surname, string email, string password)
    {
        if (id != idDto)
        {
            throw new Exception("ID no coincide");
        }

        var user = _userRepository.GetById(id) ?? throw new Exception("Usuario no encontrado.");

        user.Username = username;
        user.Name = name;
        user.Email = email;
        user.Surname = surname;
        user.Password = password;

        _userRepository.Update(user);
        _userRepository.SaveChanges();
        return UserDto.Create(user);
    }

    public void DeleteUser(int id)
    {
        _userRepository.Delete(id);
        _userRepository.SaveChanges();
    }
}