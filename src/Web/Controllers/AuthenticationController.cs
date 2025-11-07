using Microsoft.AspNetCore.Mvc;
using Core.Interfaces;
using SpendWise.Web.Models.Requests;

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
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

        // [HttpPost("register")]
        // public ActionResult<string> Register([FromBody] string username, string name, string surname, string email, string password)
        // {
            // var user = 
            // return Ok(); // devuelve un nuevo usuario
        // }
        
    }
}