
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Features.Viaticos.Queries.GetAllViaticoPais;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Viaticos
{
    [Route("api/Paises")]
    [ApiController]
    public class ViaticoPaisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ViaticoPaisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<Result<List<ViaticoPaisDto>>>> GetAll()
        {
            return await _mediator.Send(new GetAllViaticoPaisQuery());
        }
    }
}
