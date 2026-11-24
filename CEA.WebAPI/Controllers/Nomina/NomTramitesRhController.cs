using CEA.WebAPI.Filters;
using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.TramitesRh;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    // Administración de Trámites de RH (portal next-tramites-cea, /rh/tramites): todos los trámites de NOM_TRAMITES
    // (sin viáticos) en cualquier estatus, con Modificar, Completar (→ 100) y Cancelar (→ 99).
    // Solo USUARIOS.TRAMITES_NIVEL = 9; lo revisa cada handler con el usuario del token (claim "userid"), no del body.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Mayusculas] // todo lo capturado se guarda en MAYÚSCULAS (Filters/MayusculasAttribute)
    public class NomTramitesRhController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomTramitesRhController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private int? Usuario() => int.TryParse(User.FindFirst("userid")?.Value, out var n) ? n : null;

        // ?tipo= (1 vacaciones, 3 permisos, 4 justificantes) y ?estatus= opcionales.
        [HttpGet("{ano}")]
        public async Task<ActionResult<Result<List<TramiteRhDto>>>> GetTodos(int ano, [FromQuery] int? tipo, [FromQuery] int? estatus)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(new GetTramitesRhQuery(u, ano, tipo, estatus));
        }

        [HttpGet("Tramite/{idTramite}")]
        public async Task<ActionResult<Result<DetalleTramiteRhDto>>> GetDetalle(int idTramite)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(new GetDetalleTramiteRhQuery(u, idTramite));
        }

        // Body: { comentarios? }.
        [HttpPost("Tramite/{idTramite}/Completar")]
        public async Task<ActionResult<Result<int>>> Completar(int idTramite, CompletarTramiteRhCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u });
        }

        // Body: { comentarios }.
        [HttpPost("Tramite/{idTramite}/Cancelar")]
        public async Task<ActionResult<Result<int>>> Cancelar(int idTramite, CancelarTramiteRhCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u });
        }

        // Body: { desde, hasta, fechaRegreso?, idEmpleadoCubre?, comentarios }.
        [HttpPut("Tramite/{idTramite}/Vacaciones")]
        public async Task<ActionResult<Result<int>>> ModificarVacaciones(int idTramite, ModificarVacacionesRhCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u, PorEmpleado = false });
        }

        // ---- Saldos de vacaciones (punto de partida de F_SALDO_VACACIONES) ----

        [HttpGet("SaldosVacaciones")]
        public async Task<ActionResult<Result<List<SaldoVacacionesRhDto>>>> GetSaldosVacaciones()
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(new GetSaldosVacacionesRhQuery(u));
        }

        [HttpGet("SaldosVacaciones/{noEmp}")]
        public async Task<ActionResult<Result<List<RegistroVacacionesRhDto>>>> GetRegistrosVacaciones(int noEmp)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(new GetRegistrosVacacionesRhQuery(u, noEmp));
        }

        // Body: { perVac: "AAAA-N", diasXDisf }. La fecha del registro es la del día.
        [HttpPost("SaldosVacaciones/{noEmp}")]
        public async Task<ActionResult<Result<SaldoVacacionesDto?>>> CreateSaldoInicial(int noEmp, CreateSaldoInicialVacacionesRhCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { NoEmp = noEmp, NoEmpActor = u });
        }

        // Solo registros de origen R.
        [HttpDelete("SaldosVacaciones/Registro/{idVacaemp}")]
        public async Task<ActionResult<Result<int>>> DeleteRegistro(int idVacaemp)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(new DeleteRegistroVacacionesRhCommand(u, idVacaemp));
        }

        // Body: { tipoIncidencia, fechaInicio, fechaFin, justificacionMotivo, idEmpleadoCubre?, comentarios }.
        [HttpPut("Tramite/{idTramite}/PermisoJustificante")]
        public async Task<ActionResult<Result<int>>> ModificarPermisoJustificante(int idTramite, ModificarPermisoJustificanteRhCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u, PorEmpleado = false });
        }
    }
}
