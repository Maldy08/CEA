using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Queries.GetDiasFestivosEmpleado;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    [Route("api/[controller]")]
    [ApiController]
    public class NomDiasFestivosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomDiasFestivosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Festivos del rango que le tocan al empleado (por tipo de empleado, sexo y oficina). Fechas AAAA-MM-DD.
        [HttpGet("Empleado/{noemp}/{desde}/{hasta}")]
        [Authorize]
        public async Task<ActionResult<Result<List<NomDiasFestivosDto>>>> GetByEmpleado(int noemp, DateTime desde, DateTime hasta)
        {
            return await _mediator.Send(new GetDiasFestivosEmpleadoQuery(noemp, desde, hasta));
        }
    }
}
