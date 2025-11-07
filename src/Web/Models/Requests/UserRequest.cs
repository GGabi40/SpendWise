namespace Web.Models.Requests;

public record CreateUserRequest(string Username, string Name, string Surname, string Email, string Password);