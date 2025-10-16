using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.UseCases.Oficios.Queries.GetContadoresDashboardByEjercicioEmpleado;
using CEA.Application.UseCases.Oficios.Queries.GetListaDashboardByEjercicioEmpleado;
using CEA.Application.UseCases.Oficios.Queries.GetListadoOficiosFunction;
using CEA.Application.UseCases.Oficios.Queries.GetOficiosCppByEjercicioFolio;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/Oficios/Funciones")]
    [ApiController]
    public class OficioFunctionController : ControllerBase
    {
        private readonly IOficioFunctions _oficioFunctions;
        private readonly IMediator _mediator;

        public OficioFunctionController(IOficioFunctions oficioFunctions, IMediator mediator)
        {
            _oficioFunctions = oficioFunctions;
            _mediator = mediator;
        }


        [HttpGet("GetListadoOficios/{ejercicio}/{eor}/{empleado}")]
        [Authorize]

        public async Task<ActionResult<Result<List<OficioDtoFunction>>>> GetListadoOficios(int ejercicio, int eor, int empleado)
        {
            return await _mediator.Send(new GetListadoOficiosFunctionQuery(ejercicio, eor, empleado));

        }

        [HttpGet("GetListadoDashboard/{ejercicio}/{empleado}")]
        [Authorize]
        public async Task<ActionResult<Result<List<OficioListaDashboardDto>>>> GetListadoDashboard(int ejercicio, int empleado)
        {
            return await _mediator.Send(new GetListaDashboardByEjercicioEmpleadoQuery(ejercicio, empleado));
        }

        [HttpGet("GetContadoresDashboard/{ejercicio}/{empleado}")]
        [Authorize]
        public async Task<ActionResult<Result<List<OficioContadoresDashboardDto>>>> GetContadoresDashboard(int ejercicio, int empleado)
        {
            return await _mediator.Send(new GetContadoresDashboardByEjercicioEmpleadoQuery(ejercicio, empleado));
        }

        [HttpGet("GetOficiosCpp/{ejercicio}/{folio}")]
       // [Authorize]
        public async Task<ActionResult<Result<List<OficioCppDto>>>> GetOficiosCpp(int ejercicio, int folio)
        {
            return await _mediator.Send(new GetOficiosCppByEjercicioFolioQuery(ejercicio, folio));
        }

    }
}
