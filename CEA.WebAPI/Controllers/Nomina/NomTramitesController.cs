using CEA.WebAPI.Filters;
using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Commands.CancelarTramite;
using CEA.Application.UseCases.Nomina.Commands.EnviarTramite;
using CEA.Application.UseCases.Nomina.Queries.GetFirmasTramite;
using CEA.Application.UseCases.Nomina.Queries.GetFormatoTramite;
using CEA.Application.UseCases.Nomina.Queries.GetTramiteById;
using CEA.Application.UseCases.Nomina.Queries.GetTramitesEmpleado;
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
