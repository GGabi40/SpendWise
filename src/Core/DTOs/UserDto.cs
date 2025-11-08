using SpendWise.Core.Entities;

namespace SpendWise.Core.DTOs;

public record UserDto(
    string Username,
    string Email,
    string Name,
    string Surname)
{
    public static UserDto Create(User user)
    {
        return new UserDto(
            user.Username,
            user.Email,
            user.Name,
            user.Surname
        );
    }
}
