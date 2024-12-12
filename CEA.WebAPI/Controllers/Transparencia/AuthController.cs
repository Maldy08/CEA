using CEA.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Transparencia
{
    [Route("api/Transparencia/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServiceTransparencia _authService;

        public AuthController(IAuthServiceTransparencia authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string user, string password)
        {
            var response = await _authService.LoginAsync(user, password);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }

        [HttpGet("validate-token")]
        public ActionResult ValidateToken(string token)
        {
            var response = _authService.ValidateToken(token);
            if (response == null)
            {
                return Unauthorized();
            }
            return Ok(response);
        }
    }
}
