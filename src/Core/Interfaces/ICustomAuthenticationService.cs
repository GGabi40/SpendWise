namespace Core.Interfaces;

public interface ICustomAuthenticationService
{
    string Authentication(string Username, string Password);
}