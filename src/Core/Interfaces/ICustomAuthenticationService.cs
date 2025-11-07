namespace Core.Interfaces;

public interface ICustomAuthenticationService
{
    string Authentication(string username, string password);
}