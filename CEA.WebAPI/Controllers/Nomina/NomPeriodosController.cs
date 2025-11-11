using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Queries.GetPeriodosNominaByTipoEjercicio;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomPeriodosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NomPeriodosController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet("GetNomPeriodosByTipoNomEjercicio/{tiponom}/{ejercicio}")]
        public async Task<ActionResult<Result<List<NomPeriodosDto>>>> GetNomPeriodosByTipoNomEjercicio( int tiponom, int ejercicio)
        {

            return await _mediator.Send(new GetPeriodosNominaByTipoEjercicio(tiponom, ejercicio));

        }
    }
}
