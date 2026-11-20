using CEA.WebAPI.Filters;
using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Firmas;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    // Bandeja de firmas (portal next-tramites-cea, /firmas): jefes y directores (USUARIOS.TRAMITES_NIVEL = 3) firman
    // o regresan los trámites donde están en VALIDA_1 (estatus 2) / VALIDA_2 (estatus 3). Ver UseCases/Nomina/Firmas.
    // El firmante es el usuario del token (claim "userid"), nunca del body.
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [Mayusculas] // todo lo capturado se guarda en MAYÚSCULAS (Filters/MayusculasAttribute)
    public class NomTramitesFirmasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomTramitesFirmasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private int? Usuario() => int.TryParse(User.FindFirst("userid")?.Value, out var n) ? n : null;

        [HttpGet("PorFirmar")]
        public async Task<ActionResult<Result<List<TramiteRhDto>>>> GetPorFirmar()
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(new GetTramitesPorFirmarQuery(u));
        }

        // Body: { comentarios? }.
        [HttpPost("Tramite/{idTramite}/Firmar")]
        public async Task<ActionResult<Result<int>>> Firmar(int idTramite, FirmarTramiteCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u });
        }

        // Body: { comentarios } (motivo, obligatorio).
        [HttpPost("Tramite/{idTramite}/Regresar")]
        public async Task<ActionResult<Result<int>>> Regresar(int idTramite, RegresarTramiteCommand command)
        {
            if (Usuario() is not int u) return Unauthorized();
            return await _mediator.Send(command with { IdTramite = idTramite, NoEmpActor = u });
        }
    }
}
