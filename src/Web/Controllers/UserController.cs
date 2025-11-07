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

        private int? GetAuthenticatedUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ??
                              User.FindFirst("sub") ??          // común en JWT
                              User.FindFirst("id") ??
                              User.FindFirst("userId");

            if (userIdClaim == null)
                return null;

            if (int.TryParse(userIdClaim.Value, out int userId))
                return userId;

            return null;
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                if (userId == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");

                var user = _userServices.GetUserInfo(userId.Value);

                if (user == null)
                    return NotFound("Usuario no encontrado.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("me")]
        public IActionResult UpdateCurrentUser([FromBody] UpdateUserRequest request)
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                if (userId == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");

                var updatedUser = _userServices.UpdateUser(
                    userId.Value,
                    request.Id,
                    request.Username,
                    request.Name,
                    request.Surname,
                    request.Email,
                    request.Password
                );

                return Ok(updatedUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("me")]
        public IActionResult DeleteCurrentUser()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                if (userId == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");

                _userServices.DeleteUser(userId.Value);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}