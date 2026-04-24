using CEA.Application.Features.GetAllUsuarios;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Bitacora
{
    [Route("api/Bitacora/Usuarios")]
    [ApiController]
    public class BitacoraUsuariosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BitacoraUsuariosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllUsuariosQuery()));
    }
}
