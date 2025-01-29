using CEA.Application.DTOs.Oficios;
using CEA.Application.UseCases.Oficios.Commands.CreateOficioResponsable;
using CEA.Application.UseCases.Oficios.Commands.DeleteOficioResponsable;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficioResponsable;
using CEA.Application.UseCases.Oficios.Queries.GetOficioResponsableByEjercicioFolioEor;
using CEA.Shared.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/OficioResponsable")]
    [ApiController]
    public class OficioResponsableController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OficioResponsableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetOficioResponsableByEjercicioFolioEor/{ejercicio}/{folio}/{eor}/{rol}")]

        public async Task<ActionResult<Result<List<OficioResponsableDto>>>> GetOficioResponsableByEjercicioFolioEor(int ejercicio, int folio, int eor, int rol)
        {
            return await _mediator.Send(new GetOficioResponsableByEjercicioFolioEorQuery(ejercicio, folio, eor, rol));
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> CreateOficioResponsable([FromBody] CreateOficioResponsableCommand command)
        {

            return await _mediator.Send(command);

        }

        [HttpPut]

        public async Task<ActionResult<Result<int>>> UpdateOficioResponsable([FromBody] UpdateOficioResponsableCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        public async Task<ActionResult<Result<int>>> DeleteOficioResponsable([FromBody] DeleteOficioResponsableCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
