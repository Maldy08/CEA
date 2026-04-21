using CEA.Application.DTOs.Checador;
using CEA.Application.UseCases.Checador.Queries;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace CEA.WebAPI.Controllers
{
    [Route("api/Checador")]
    [ApiController]
    public class ChecadorController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChecadorController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("probar-conexion")]
        //[Authorize]
        // 1. CAMBIO: Actualiza el tipo de retorno a <RegistroAsistenciaDto>
        public async Task<ActionResult<Result<List<RegistroAsistenciaDto>>>> ProbarConexion(
            [FromQuery] string deviceKey,
            [FromQuery] DateTime startTime,
            [FromQuery] DateTime endTime)
        {
            if (string.IsNullOrWhiteSpace(deviceKey))
            {
                // 2. CAMBIO: Actualiza el tipo de Falla
                return BadRequest(Result<List<RegistroAsistenciaDto>>.Failure("Debe especificar 'deviceKey' (ej. ChecadorMxli o ChecadorArct)."));
            }

            if (startTime == default || endTime == default)
            {
                // 3. CAMBIO: Actualiza el tipo de Falla
                return BadRequest(Result<List<RegistroAsistenciaDto>>.Failure("Debe especificar 'startTime' y 'endTime'."));
            }

            if (endTime <= startTime)
            {
                // 4. CAMBIO: Actualiza el tipo de Falla
                return BadRequest(Result<List<RegistroAsistenciaDto>>.Failure("'endTime' debe ser posterior a 'startTime'."));
            }

            var query = new GetAcsEventsFromDeviceQuery(deviceKey, startTime, endTime);
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}