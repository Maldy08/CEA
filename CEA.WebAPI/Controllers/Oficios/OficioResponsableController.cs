using CEA.Application.DTOs.Oficios;
using CEA.Application.UseCases.Oficios.Commands.CreateOficioResponsable;
using CEA.Application.UseCases.Oficios.Commands.DeleteOficioResponsable;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficioResponsable;
using CEA.Application.UseCases.Oficios.Queries.GetOficioResponsableByEjercicioFolioEor;
using CEA.Application.UseCases.Oficios.Queries.GetOficiosResponsableByEjercicioFolioEor;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]

        public async Task<ActionResult<Result<List<OficioResponsableDto>>>> GetOficioResponsableByEjercicioFolioEor(int ejercicio, int folio, int eor, int rol)
        {
            return await _mediator.Send(new GetOficioResponsableByEjercicioFolioEorQuery(ejercicio, folio, eor, rol));
        }

        [HttpGet("GetOficioResponsablesByEjercicioFolioEorNew/{ejercicio}/{folio}/{eor}")]
        [Authorize]
        public async Task<ActionResult<Result<IEnumerable<OficioResponsableDto>>>>
            GetOficioResponsablesByEjercicioFolioEor(int ejercicio, int folio, int eor)
        {
            return await _mediator.Send(new GetOficiosResponsableByEjercicioFolioEorQuery(ejercicio, folio, eor));
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Result<int>>> CreateOficioResponsable([FromBody] CreateOficioResponsableCommand command)
        {

            return await _mediator.Send(command);

        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<Result<int>>> UpdateOficioResponsable([FromBody] UpdateOficioResponsableArrayCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Result<int>>> DeleteOficioResponsable([FromBody] DeleteOficioResponsableCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
