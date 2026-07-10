using CEA.Application.UseCases.IndiArct.Energia.Commands.CaptureEnergiaLote;
using CEA.Application.UseCases.IndiArct.Energia.Commands.CreateEnergia;
using CEA.Application.UseCases.IndiArct.Energia.Commands.DeleteEnergia;
using CEA.Application.UseCases.IndiArct.Energia.Commands.UpdateEnergia;
using CEA.Application.UseCases.IndiArct.Energia.Queries.GetAllEnergia;
using CEA.Application.UseCases.IndiArct.Energia.Queries.GetEnergiaById;
using CEA.Application.UseCases.IndiArct.Energia.Queries.GetEnergiaGrid;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.IndiArct
{
    [Route("api/IndiArct/Energia")]
    [ApiController]
    public class EnergiaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EnergiaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllEnergiaQuery()));

        // Formulario de captura: los 12 meses del año + sus valores (null si no hay)
        [HttpGet("GetGrid/{anio}")]
        public async Task<IActionResult> GetGrid(int anio)
            => Ok(await _mediator.Send(new GetEnergiaGridQuery(anio)));

        // Guardado por lote de todo el año (upsert por año+mes)
        [HttpPost("CaptureLote")]
        public async Task<IActionResult> CaptureLote([FromBody] CaptureEnergiaLoteCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetEnergiaByIdQuery(id)));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEnergiaCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateEnergiaCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteEnergiaCommand(id)));
    }
}
