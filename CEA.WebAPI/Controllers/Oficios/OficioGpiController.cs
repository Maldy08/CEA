using CEA.Application.DTOs.Oficios;
using CEA.Application.UseCases.Oficios.Queries.GetAllOficioGpi;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/[controller]")]
    [ApiController]
    public class OficioGpiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OficioGpiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Result<IEnumerable<OficioGpiDto>>>> GetAllOficioGpi()
        {
            return await _mediator.Send(new GetAllOficioGpiQuery());
        }
    }
}
