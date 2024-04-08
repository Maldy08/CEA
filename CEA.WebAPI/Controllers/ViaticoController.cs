using CEA.Application.DTOs;
using CEA.Application.Features.Viaticos.Commands.CreateViatico;
using CEA.Application.Features.Viaticos.Queries.GetAllByEjercicioDepto;
using CEA.Application.Features.Viaticos.Queries.GetAllViaticosByEjercicioAndOficina;
using CEA.Application.Features.Viaticos.Queries.GetNoViat;
using CEA.Shared.Interfaces;
using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViaticoController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public ViaticoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAllViaticosByEjercicioAndOficina")]
        public async Task<ActionResult<Result<List<GetAllViaticosDto>>>> GetAllViaticosByEjercicioAndOficina(int ejercicio, int oficina)
        {
            return await _mediator.Send(new GetAllViaticosByEjercicioAndOficinaQuery(ejercicio, oficina));
        }

        [HttpPost("CreateViatico")]
        public async Task<ActionResult<Result<int>>> CreateViatico(CreateViaticoCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet("GetNoViat")]
        public async Task<ActionResult<Result<int>>> GetNoViat(int ejercicio, int oficina)
        {
            return await _mediator.Send(new GetNoViatQuery(ejercicio, oficina));
        }

        [HttpGet("GetAllByEjercicioDepto")]
        public async Task<ActionResult<Result<List<GetAllViaticosDto>>>> GetAllByEjercicioDepto(int ejercicio, int departamento)
        {
            return await _mediator.Send(new GetAllByEjercicioDeptoQuery(ejercicio, departamento));
        }


    }
}
