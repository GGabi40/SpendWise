using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using SpendWise.Web.Models.Requests;
using SpendWise.Core.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ICustomAuthenticationService _customAuthenticationService;

        public AuthenticationController(ICustomAuthenticationService customAuthenticationService)
        {
            _customAuthenticationService = customAuthenticationService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
        {
            var dto = new UserRegisterDto
            {
                Username = request.Username,
                Email = request.Email,
                Password = request.Password,
                Name = request.Name,
                Surname = request.Surname
            };

            try
            {
                var user = await _customAuthenticationService.RegisterAsync(dto);
                var response = UserDto.Create(user);
                return Ok(user);
            } catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPost("login")]
        public ActionResult<string> Authenticate([FromBody] AuthenticationRequest request)
        {
            try
            {
                var token = _customAuthenticationService.Authentication(
                    request.Username,
                    request.Password
                );
                return Ok(new { token });
            } catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}