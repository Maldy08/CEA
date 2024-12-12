using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Queries.GetAllOficiosUsuExt;
using CEA.Application.UseCases.Oficios.Commands.CreateOficioUsuExt;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/OficioUsuExt")]
    [ApiController]
    public class OficioUsuExtController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OficioUsuExtController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OficioUsuExtDto>>> GetAllOficiosUsuExt()
        {
            return Ok(await _mediator.Send(new GetAllOficiosUsuExtQuery()));
        }

        [HttpPost]
        public async Task<IActionResult> CreateOficioUsuExt([FromBody] CreateOficioUsuExtCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);

        }
    }
}
