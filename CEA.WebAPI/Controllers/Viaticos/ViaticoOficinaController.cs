using CEA.Application.DTOs.Viaticos;
using CEA.Application.Features.Viaticos.Queries.GetAllViaticoOficina;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{
    [Route("api/Oficinas")]
    [ApiController]
    public class ViaticoOficinaController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViaticoOficinaController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<ViaticoOficinaDto>>>> GetAll()
        {
            return await _mediator.Send(new GetAllViaticoOficinaQuery());
        }
    }
}
