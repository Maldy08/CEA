using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Queries.GetAllOficiosEstatus;
using CEA.Application.Features.Oficios.Queries.GetOficiosEstatusByIdEor;
using CEA.Shared.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/OficioEstatus")]
    [ApiController]
    public class OficioEstatusController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OficioEstatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<OficioEstatusDto>>>> GetAllOficioEstatus()
        {
            return await _mediator.Send(new GetAllOficiosEstatusQuery());
        }

        [HttpGet("GetOficioEstatusByIdEor/{id}/{eor}")]
        public async Task<ActionResult<Result<OficioEstatusDto>>> GetOficioEstatusByIdEor(int id, int eor)
        {
            return await _mediator.Send(new GetOficiosEstatusByIdEorQuery(id, eor));
        }

    }
}
