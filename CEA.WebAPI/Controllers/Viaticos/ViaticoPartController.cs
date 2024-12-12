using CEA.Application.DTOs.Viaticos;
using CEA.Application.Features.Viaticos.Commands.CreateViaticoPart;
using CEA.Application.Features.Viaticos.Commands.UpdateViaticoPart;
using CEA.Application.Features.Viaticos.Queries.GetViaticoPartByOficinaEjercicioNoviatPartida;

using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{
    [Route("api/Viatico/ViaticosPart")]
    public class ViaticoPartController : ApiControllerBaseViaticos
    {

        private readonly IMediator _mediator;

        public ViaticoPartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetByOficinaEjercicioNoviatPartida")]
        public async Task<ActionResult<Result<ViaticoPartDto>>> GetByOficinaEjercicioNoviatPartida(int oficina, int ejercicio, int noviat, int partida)
        {
            return await _mediator.Send(new GetViaticoPartByOficinaEjercicioNoviatPartidaQuery(oficina, ejercicio, noviat, partida));
        }

        [HttpPost]
        public async Task<ActionResult<Result<int>>> Create(CreateViaticoPartCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult<Result<int>>> Update(UpdateViaticoPartCommand command)
        {
            return await _mediator.Send(command);
        }

    }
}
