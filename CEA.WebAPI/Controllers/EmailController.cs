
using CEA.Application.Features.Email.Commands.SendEmail;

using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers
{
    [Route("api/Email")]
    [ApiController]
    public class EmailController : ControllerBase
    {
       [HttpPost]
       public async Task<ActionResult> SendEmail([FromServices] IMediator mediator, [FromForm] SendEmailCommand command)
        {
            var result = await mediator.Send(command);
            return Ok(result);
        }
    }
}
