using CEA.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string user, string password)
        {
            try
            {
                var response = await _authService.LoginAsync(user, password);
                if (response == null)
                {
                    return Unauthorized();
                }
                return Ok(response);
            }
            catch (Exception)
            {
                return BadRequest("Ocurrio un problema inesperado");
                throw;
            }
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
