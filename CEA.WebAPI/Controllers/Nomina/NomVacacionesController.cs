using CEA.WebAPI.Filters;
using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Commands.CreateSolicitudVacaciones;
using CEA.Application.UseCases.Nomina.Queries.GetConsultaVacaciones;
using CEA.Application.UseCases.Nomina.Queries.GetSaldoVacaciones;
using CEA.Application.UseCases.Nomina.Queries.GetSimulaVacaciones;
using CEA.Application.UseCases.Nomina.Queries.GetSolicitudVacaciones;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    [Route("api/[controller]")]
    [ApiController]
    [Mayusculas] // todo lo capturado se guarda en MAYÚSCULAS (Filters/MayusculasAttribute)
    public class NomVacacionesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomVacacionesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Reparte `dias` solicitados entre los periodos del empleado (F_SIMULA_VACACIONES). Solo consulta.
        [HttpGet("SimulaVacaciones/{noemp}/{dias}")]
        [Authorize]
        public async Task<ActionResult<Result<List<SimulaVacacionesDto>>>> SimulaVacaciones(int noemp, int dias)
        {
            return await _mediator.Send(new GetSimulaVacacionesQuery(noemp, dias));
        }

        // Solicitud de vacaciones de un rango (FN_CONSULTA_VACACIONES): días hábiles sin fines de semana ni festivos
        // del empleado, festivos que no se contaron y reparto entre periodos. Solo consulta. Fechas AAAA-MM-DD.
        [HttpGet("ConsultaVacaciones/{noemp}/{desde}/{hasta}")]
        [Authorize]
        public async Task<ActionResult<Result<ConsultaVacacionesDto>>> ConsultaVacaciones(int noemp, DateTime desde, DateTime hasta)
        {
            return await _mediator.Send(new GetConsultaVacacionesQuery(noemp, desde, hasta));
        }

        // Saldo de vacaciones a hoy (F_SALDO_VACACIONES): primer periodo con saldo y total pendiente.
        [HttpGet("SaldoVacaciones/{noemp}")]
        [Authorize]
        public async Task<ActionResult<Result<SaldoVacacionesDto>>> SaldoVacaciones(int noemp)
        {
            return await _mediator.Send(new GetSaldoVacacionesQuery(noemp));
        }

        // Solicitud de vacaciones guardada de un trámite (NOM_VACAEMP_M) con su reparto por periodo (NOM_VACAEMP_D).
        [HttpGet("Solicitud/{idTramite}")]
        [Authorize]
        public async Task<ActionResult<Result<SolicitudVacacionesDto>>> Solicitud(int idTramite)
        {
            return await _mediator.Send(new GetSolicitudVacacionesQuery(idTramite));
        }

        // Genera la solicitud (estatus 1): trámite + bitácora + NOM_VACAEMP_M/_D en una transacción, folio VAC-NNNN-AAAA.
        [HttpPost("Solicitud")]
        [Authorize]
        public async Task<ActionResult<Result<SolicitudVacacionesCreadaDto>>> CreateSolicitud(CreateSolicitudVacacionesCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
