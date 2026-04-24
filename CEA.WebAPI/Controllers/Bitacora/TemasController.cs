using CEA.Application.UseCases.Bitacora.Temas.Commands.CreateTema;
using CEA.Application.UseCases.Bitacora.Temas.Commands.DeleteTema;
using CEA.Application.UseCases.Bitacora.Temas.Commands.UpdateTema;
using CEA.Application.UseCases.Bitacora.Temas.Commands.UpdateTemaEstado;
using CEA.Application.UseCases.Bitacora.Temas.Queries.GetAllTemas;
using CEA.Application.UseCases.Bitacora.Temas.Queries.GetContadores;
using CEA.Application.UseCases.Bitacora.Temas.Queries.GetTemaById;
using CEA.Application.UseCases.Bitacora.Temas.Queries.GetTemasByUsuario;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Bitacora
{
    [Route("api/Bitacora/Temas")]
    [ApiController]
    public class TemasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TemasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllTemasQuery()));

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetTemaByIdQuery(id)));

        [HttpGet("GetByUsuario/{id}")]
        public async Task<IActionResult> GetByUsuario(int id)
            => Ok(await _mediator.Send(new GetTemasByUsuarioQuery(id)));

        [HttpGet("GetContadores")]
        public async Task<IActionResult> GetContadores()
            => Ok(await _mediator.Send(new GetContadoresQuery()));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateTemaCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateTemaCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("UpdateEstado/{id}/{estado}")]
        public async Task<IActionResult> UpdateEstado(int id, string estado)
            => Ok(await _mediator.Send(new UpdateTemaEstadoCommand(id, estado)));

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteTemaCommand(id)));
    }
}
