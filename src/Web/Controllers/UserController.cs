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
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
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

        [HttpGet()]
        public IActionResult GetCurrentUser()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                if (userId == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");

                var user = _userService.GetUserInfo(userId.Value);

                if (user == null)
                    return NotFound("Usuario no encontrado.");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete()]
        public async Task<IActionResult> DeleteCurrentUser()
        {
            try
            {
                var userId = GetAuthenticatedUserId();
                if (userId == null)
                    return Unauthorized("No se pudo determinar el usuario autenticado.");

                await _userService.DeleteUser(userId.Value);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}