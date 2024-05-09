using CEA.Application.DTOs.Viaticos;
using CEA.Application.Features.Viaticos.Queries.GetViaticoPartByOficinaEjercicioNoviatPartida;
using CEA.Domain.Entities.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{
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

    }
}
