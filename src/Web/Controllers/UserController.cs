using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Core.Services;
using SpendWise.Core.DTOs;
using System.Security.Claims;
using SpendWise.Web.Models.Requests;

namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserServices _userServices;

        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                                    User.FindFirst("id") ??
                                    User.FindFirst("userId");

                if (userIdClaim == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");

                int userId = int.Parse(userIdClaim.Value);
                var user = _userServices.GetUserInfo(userId);

                if (user == null)
                    return NotFound("Usuario no encontrado.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("me")]
        public IActionResult UpdateCurrentUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                                    User.FindFirst("id") ??
                                    User.FindFirst("userId");

                if (userIdClaim == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado");

                int userId = int.Parse(userIdClaim.Value);

                var updateUser = _userServices.UpdateUser(
                    userId,
                    request.Id,
                    request.Username,
                    request.Name,
                    request.Surname,
                    request.Email,
                    request.Password
                );
                return Ok(updateUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("me")]
        public IActionResult DeleteCurrentUser()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                                    User.FindFirst("id") ??
                                    User.FindFirst("userId");

                if (userIdClaim == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");
                int userId = int.Parse(userIdClaim.Value);

                _userServices.DeleteUser(userId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
    public class UpdateUserRequest
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}