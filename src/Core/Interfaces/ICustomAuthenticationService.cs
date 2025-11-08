using SpendWise.Core.DTOs;
using SpendWise.Core.Entities;

namespace Core.Interfaces;

public interface ICustomAuthenticationService
{
    string Authentication(string username, string password);
    Task<User> RegisterAsync(UserRegisterDto dto);
}