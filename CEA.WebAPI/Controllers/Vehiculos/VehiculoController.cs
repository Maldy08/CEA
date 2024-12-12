using CEA.Application.Features.Vehiculos.Queries.GetListaVehiculos;
using CEA.Application.Features.Vehiculos.Queries.GetVehiculoByNoEconomico;
using CEA.Application.Features.Vehiculos.Queries.GetVehiculos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Vehiculos
{
    [Route("api/Vehiculos")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiculoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetVehiculos()
        {
            var query = new GetVehiculosQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehiculo(int id)
        {
            var query = new GetVehiculoByNoEconomicoQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetListaVehiculos")]
        public async Task<IActionResult> GetListaVehiculos()
        {
            var query = new GetListaVehiculosQuery() ;
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
