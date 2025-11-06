using SpendWise.Core.Entities;

namespace SpendWise.Core.DTOs;

public record UserDto(int Id, string Username, string Email, string Name, string Surname)
{
    public static UserDto Create(User user)
    {
        return new UserDto(
            user.Id,
            user.Username,
            user.Email,
            user.Name,
            user.Surname
        );
    }
}
