using CEA.Shared.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.Succeeded)
            {
                return Ok(result);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}
