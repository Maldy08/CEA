using CEA.WebAPI.Filters;
using CEA.Application.DTOs.Nomina;
using CEA.Application.UseCases.Nomina.Commands.CreatePermisoJustificante;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Nomina
{
    // Permisos (TIPO_TRAMITE 3) y justificantes (4): NOM_TRAMITES + NOM_PERMISOS_JUSTIFICANTES.
    // Consulta, envío y cancelación van por NomTramites, igual que vacaciones.
    [Route("api/[controller]")]
    [ApiController]
    [Mayusculas] // todo lo capturado se guarda en MAYÚSCULAS (Filters/MayusculasAttribute)
    public class NomPermisosJustificantesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NomPermisosJustificantesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Genera el trámite (estatus 1) con folio PER-/JUS-NNNN-AAAA según el tipo de incidencia.
        [HttpPost("Solicitud")]
        [Authorize]
        public async Task<ActionResult<Result<PermisoJustificanteCreadoDto>>> CreateSolicitud(CreatePermisoJustificanteCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
