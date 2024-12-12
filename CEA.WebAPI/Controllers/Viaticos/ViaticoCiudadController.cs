using CEA.Application.DTOs.Viaticos;
using CEA.Application.Features.Viaticos.Queries.GetAllViaticoCiudades;
using CEA.Application.Features.Viaticos.Queries.GetViaticoCiudadesByEstado;
using CEA.Application.Features.Viaticos.Queries.GetViaticoCiudadesById;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{
    [Route("api/Viaticos/Ciudades")]
    public class ViaticoCiudadController : ApiControllerBaseViaticos
    {
        private readonly IMediator _mediator;

        public ViaticoCiudadController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<Result<List<ViaticoCiudadDto>>>> GetAll()
        {
            return await _mediator.Send(new GetAllViaticoCiudadesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Result<ViaticoCiudadDto>>> GetById(int id)
        {
            return await _mediator.Send(new GetViaticoCiudadesByIdQuery(id));
        }

        [HttpGet("Estado/{idEstado}")]
        public async Task<ActionResult<Result<List<ViaticoCiudadDto>>>> GetByIdEstado(int idEstado)
        {
            return await _mediator.Send(new GetViaticoCiudadesByEstadoQuery(idEstado));
        }
    }
}
