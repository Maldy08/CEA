using CEA.Application.Features.GetAllEmpleados;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Bitacora
{
    [Route("api/Bitacora/Empleados")]
    [ApiController]
    public class BitacoraEmpleadosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BitacoraEmpleadosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllEmpleadosQuery()));
    }
}
