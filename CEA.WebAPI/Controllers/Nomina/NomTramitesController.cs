using CEA.WebAPI.Filters;
using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Commands.CancelarTramite;
using CEA.Application.UseCases.Nomina.Commands.EnviarTramite;
using CEA.Application.UseCases.Nomina.Queries.GetFirmasTramite;
using CEA.Application.UseCases.Nomina.Queries.GetFormatoTramite;
using CEA.Application.UseCases.Nomina.Queries.GetTramiteById;
using CEA.Application.UseCases.Nomina.Queries.GetTramitesEmpleado;
using CEA.Application.UseCases.Nomina.Queries.GetTramitesPersonal;
using CEA.Application.UseCases.Nomina.TramitesRh;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    // Trámites de RH (NOM_TRAMITES): consulta y cambios de estatus (pestaña "Procesos").
    [Route("api/[controller]")]
    [ApiController]
    [Mayusculas] // todo lo capturado se guarda en MAYÚSCULAS (Filters/MayusculasAttribute)
    public class NomTramitesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomTramitesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Trámites del empleado en el año. ?tipo= filtra por tipo de trámite (1 vacaciones…).
        [HttpGet("Empleado/{noemp}/{ano}")]
        [Authorize]
        public async Task<ActionResult<Result<List<NomTramiteDto>>>> GetByEmpleado(int noemp, int ano, [FromQuery] int? tipo)
        {
            return await _mediator.Send(new GetTramitesEmpleadoQuery(noemp, ano, tipo));
        }

        // Trámites en el año del personal a cargo del usuario (claim "userid"; solo TRAMITES_NIVEL 2). ?tipo= filtra.
        [HttpGet("Personal/{ano}")]
        [Authorize]
        public async Task<ActionResult<Result<List<NomTramiteDto>>>> GetDePersonal(int ano, [FromQuery] int? tipo)
        {
            if (!int.TryParse(User.FindFirst("userid")?.Value, out var noEmpUsuario))
                return Unauthorized();
            return await _mediator.Send(new GetTramitesPersonalQuery(noEmpUsuario, ano, tipo));
        }

        // Trámites en el año que el usuario capturó para otros (ID_EMPLEADO_SUPERVISOR = claim "userid"), p. ej. en
        // suplencia de otro supervisor. Solo TRAMITES_NIVEL 2. ?tipo= filtra.
        [HttpGet("Capturados/{ano}")]
        [Authorize]
        public async Task<ActionResult<Result<List<NomTramiteDto>>>> GetCapturados(int ano, [FromQuery] int? tipo)
        {
            if (!int.TryParse(User.FindFirst("userid")?.Value, out var noEmpUsuario))
                return Unauthorized();
            return await _mediator.Send(new GetTramitesPersonalQuery(noEmpUsuario, ano, tipo, Capturados: true));
        }

        // Quién firma el trámite del empleado según su departamento (FN_TRAMITES_CONSULTA_FIRMAS).
        [HttpGet("Firmas/{noemp}/{depto}")]
        [Authorize]
        public async Task<ActionResult<Result<FirmasTramiteDto>>> GetFirmas(int noemp, int depto)
        {
            return await _mediator.Send(new GetFirmasTramiteQuery(noemp, depto));
        }

        // Trámite con su bitácora de movimientos.
        [HttpGet("{idTramite}")]
        [Authorize]
        public async Task<ActionResult<Result<NomTramiteDto>>> GetById(int idTramite)
        {
            return await _mediator.Send(new GetTramiteByIdQuery(idTramite));
        }

        // Datos del formato oficial impreso (VS_TRAMITES_FORMATO + _DET), igual para todos los tipos de trámite.
        // Solo para el empleado del trámite, sus firmantes o su supervisor (usuario = claim "userid" del token).
        [HttpGet("{idTramite}/Formato")]
        [Authorize]
        public async Task<ActionResult<Result<FormatoTramiteDto>>> GetFormato(int idTramite)
        {
            if (!int.TryParse(User.FindFirst("userid")?.Value, out var noEmpUsuario))
                return Unauthorized();
            return await _mediator.Send(new GetFormatoTramiteQuery(idTramite, noEmpUsuario));
        }

        // ---- Edición por el empleado o su supervisor (Bandeja): mismos comandos que RH con PorEmpleado = true ----
        // Solo el beneficiario o quien lo supervisa (SupervisaA); modificar solo en Creado (1) o Regresado (98).

        // Trámite con bitácora y su detalle (vacaciones o permiso/justificante) para editarlo.
        [HttpGet("{idTramite}/Detalle")]
        [Authorize]
        public async Task<ActionResult<Result<DetalleTramiteRhDto>>> GetDetalle(int idTramite)
        {
            if (!int.TryParse(User.FindFirst("userid")?.Value, out var u)) return Unauthorized();
            return await _mediator.Send(new GetDetalleTramiteRhQuery(u, idTramite, PorEmpleado: true));
        }

        // Body: { desde, hasta, fechaRegreso?, idEmpleadoCubre?, comentarios? }.
        [HttpPut("{idTramite}/Vacaciones")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> ModificarVacaciones(int idTramite, ModificarVacacionesRhCommand command)
        {
            if (!int.TryParse(User.FindFirst("userid")?.Value, out var u)) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u, PorEmpleado = true });
        }

        // Body: { tipoIncidencia, fechaInicio, fechaFin, justificacionMotivo, idEmpleadoCubre?, comentarios? }.
        [HttpPut("{idTramite}/PermisoJustificante")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> ModificarPermisoJustificante(int idTramite, ModificarPermisoJustificanteRhCommand command)
        {
            if (!int.TryParse(User.FindFirst("userid")?.Value, out var u)) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u, PorEmpleado = true });
        }

        // Envío al jefe para su autorización (1 o 98 → 2). Body: { noEmpActor }.
        [HttpPost("{idTramite}/Enviar")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> Enviar(int idTramite, EnviarTramiteCommand command)
        {
            return await _mediator.Send(command with { IdTramite = idTramite });
        }

        // Cancelación (1, 2 o 98 → 99). Body: { noEmpActor, comentarios }.
        [HttpPost("{idTramite}/Cancelar")]
        [Authorize]
        public async Task<ActionResult<Result<int>>> Cancelar(int idTramite, CancelarTramiteCommand command)
        {
            return await _mediator.Send(command with { IdTramite = idTramite });
        }
    }
}
