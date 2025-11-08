using System.Text;
using Core.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using SpendWise.Core.Entities;
using SpendWise.Core.Interfaces;
using System.Security.Claims;
using SpendWise.Core.DTOs;

namespace Infrastructure.Services;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly AutenticacionServiceOptions _options;

    public CustomAuthenticationService(IUserRepository userRepository, IOptions<AutenticacionServiceOptions> options)
    {
        _userRepository = userRepository;
        _options = options.Value;
    }

    private async Task<User?> ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            return null;

        var user = await _userRepository.GetUserByUsernameAsync(username);

        if (user == null)
            return null;

        if (user.Password == password)
            return user;

        return null;
    }

    public string Authentication(string username, string password)
    {
        var userTask = ValidateUser(username, password);
        var user = userTask.Result;

        if (user == null)
        {
            throw new Exception("Falló la autenticación de usuario.");
        }

        // Crea token con JWT
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_options.SecretForKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.Username),
                new Claim("family_name", user.Surname),
            };

        var jwtToken = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddHours(1),
            credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public async Task<User> RegisterAsync(UserRegisterDto dto)
    {
        var existingUser = await _userRepository.GetUserByUsernameAsync(dto.Username);

        if (existingUser != null)
            throw new Exception("Este username ya existe");

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            Name = dto.Name,
            Surname = dto.Surname
        };

        user.SetPassword(dto.Password);

        await _userRepository.AddAsync(user);
        return user;
    }

    // Opciones de configuración
    public class AutenticacionServiceOptions
    {
        public const string SectionName = "AutenticationService";
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecretForKey { get; set; } = string.Empty;
    }
}