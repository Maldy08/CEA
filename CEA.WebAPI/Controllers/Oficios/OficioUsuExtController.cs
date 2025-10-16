using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Queries.GetAllOficiosUsuExt;
using CEA.Application.UseCases.Oficios.Commands.CreateOficioUsuExt;
using CEA.Application.UseCases.Oficios.Commands.UpdateOficioUsuExt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
         //[Authorize]
        public async Task<ActionResult<IEnumerable<OficioUsuExtDto>>> GetAllOficiosUsuExt()
        {
            return Ok(await _mediator.Send(new GetAllOficiosUsuExtQuery()));
        }

        [HttpGet("mantenimiento")]
        //[Authorize]
        public async Task<ActionResult<IEnumerable<OficioUsuExtDto>>> GetAllOficiosUsuExtManto()
        {
            return Ok(await _mediator.Send(new GetAllOficiosUsuExtQueryManto()));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOficioUsuExt([FromBody] CreateOficioUsuExtCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);

        }

        [HttpPut]
        //[Authorize]
        public async Task<IActionResult> UpdateOficioUsuExt([FromBody] UpdateOficioUsuExtCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
