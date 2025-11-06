using Core.Interfaces;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;

namespace Infrastructure.Services;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly IUserRepository _userRepository;

    public CustomAuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    private User? ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        var user = _userRepository.GetUserByUsername(username);

        if (user == null)
        {
            return null;
        }

        if (user.Password == password)
        {
            return user;
        }

        return null;
    }

    public string Authentication(string username, string password)
    {
        var user = ValidateUser(username, password);

        if (user == null)
        {
            throw new Exception("Falló la autenticación de usuario.");
        }

        // JWT
        return null;
    }
}