
using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Queries.GetOficiosBitacoraByEjercicioFolioEor;
using CEA.Application.UseCases.Oficios.Commands.CreateOficioBitacora;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/OficioBitacora")]
    [ApiController]
    public class OficioBitacoraController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OficioBitacoraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetOficioBitacoraByEjercicioFolioEor/{ejercicio}/{folio}/{eor}")]
        [Authorize]
        public async Task<ActionResult<Result<List<OficioBitacoraDto>>>> GetOficioBitacoraByEjercicioFolioEor(int ejercicio, int folio, int eor)
        {
            return await _mediator.Send(new GetOficiosBitacoraByEjercicioFolioEorQuery(ejercicio, folio, eor));
        }

        [HttpPost]
        [Authorize]

        public async Task<ActionResult<Result<int>>> CreateOficioBitacora([FromBody] CreateOficioBitacoraCommand command)
        {
            return new JsonResult(await _mediator.Send(command));
        }


        [HttpPost("CreateBitacoraSP")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> CreateOficioBitacoraSP([FromBody] CreateOficioBitacoraSPCommand command)
        {
            return new JsonResult(await _mediator.Send(command));
        }

    }
}
