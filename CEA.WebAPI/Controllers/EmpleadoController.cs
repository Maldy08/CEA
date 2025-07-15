
using CEA.Application.DTOs;
using CEA.Application.Features.GetAllEmpleados;
using CEA.Application.Features.GetAllEmpleadosByDeptoComi;
using CEA.Application.Features.GetAllEmpleadosByDeptoPpto;
using CEA.Application.Features.GetEmpleadoById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers
{
    [Route("api/Empleados")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        // private readonly IEmpleadoRepository _repository;
        private readonly IMediator _mediator;

        public EmpleadoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<EmpleadoDto>>> GetEmpleados()
        {
            return Ok(await _mediator.Send(new GetAllEmpleadosQuery()));
        }

        [HttpGet("GetEmpleadoById/{id}")]
        //[Authorize]

        public async Task<ActionResult<EmpleadoDto>> GetEmpleadoById(int id)
        {
            return Ok(await _mediator.Send(new GetEmpleadoByIdQuery(id)));

        }

        [HttpGet("GetEmpleadosByDeptoPpto/{id}")]
        //[Authorize]

        public async Task<ActionResult<List<EmpleadoDto>>> GetEmpleadosByDeptoPpto(int id)
        {
            return Ok(await _mediator.Send(new GetAllEmpleadosByDeptoPptoQery(id)));
        }

        [HttpGet("GetEmpleadosByDeptoComi/{id}")]
        //[Authorize]

        public async Task<ActionResult<List<EmpleadoDto>>> GetEmpleadosByDeptoComi(int id)
        {
            return Ok(await _mediator.Send(new GetAllEmpleadosByDeptoComiQuery(id)));

        }

    }
}
