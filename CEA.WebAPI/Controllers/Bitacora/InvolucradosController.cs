using CEA.Application.UseCases.Bitacora.Involucrados.Commands.AssignInvolucrado;
using CEA.Application.UseCases.Bitacora.Involucrados.Commands.RemoveInvolucrado;
using CEA.Application.UseCases.Bitacora.Involucrados.Queries.GetInvolucradosByTema;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Bitacora
{
    [Route("api/Bitacora/Involucrados")]
    [ApiController]
    public class InvolucradosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvolucradosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetByTema/{id}")]
        public async Task<IActionResult> GetByTema(int id)
            => Ok(await _mediator.Send(new GetInvolucradosByTemaQuery(id)));

        [HttpPost("Assign")]
        public async Task<IActionResult> Assign([FromBody] AssignInvolucradoCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("Remove/{idTema}/{idUsuario}")]
        public async Task<IActionResult> Remove(int idTema, int idUsuario)
            => Ok(await _mediator.Send(new RemoveInvolucradoCommand(idTema, idUsuario)));
    }
}
