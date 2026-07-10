using CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.CapturePresasLote;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.CreatePresaNivel;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.DeletePresaNivel;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.UpdatePresaNivel;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetAllPresasNiveles;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresaNivelById;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresasGrid;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresasNivelesByAnio;
using CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresasNivelesByPresa;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.IndiArct
{
    [Route("api/IndiArct/PresasNiveles")]
    [ApiController]
    public class PresasNivelesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PresasNivelesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllPresasNivelesQuery()));

        // Formulario de captura: todas las presas + su volumen del año/mes (null si no hay)
        [HttpGet("GetGrid/{anio}/{mes}")]
        public async Task<IActionResult> GetGrid(int anio, int mes)
            => Ok(await _mediator.Send(new GetPresasGridQuery(anio, mes)));

        // Guardado por lote de todo el formulario (upsert por año+mes+presa)
        [HttpPost("CaptureLote")]
        public async Task<IActionResult> CaptureLote([FromBody] CapturePresasLoteCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("GetByPresa/{idPresa}")]
        public async Task<IActionResult> GetByPresa(int idPresa)
            => Ok(await _mediator.Send(new GetPresasNivelesByPresaQuery(idPresa)));

        // Consulta/reporte anual: todas las capturas del año
        [HttpGet("GetByAnio/{anio}")]
        public async Task<IActionResult> GetByAnio(int anio)
            => Ok(await _mediator.Send(new GetPresasNivelesByAnioQuery(anio)));

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetPresaNivelByIdQuery(id)));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreatePresaNivelCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdatePresaNivelCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeletePresaNivelCommand(id)));
    }
}
