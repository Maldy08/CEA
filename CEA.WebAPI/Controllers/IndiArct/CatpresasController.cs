using CEA.Application.UseCases.IndiArct.Catpresas.Commands.CreateCatpresa;
using CEA.Application.UseCases.IndiArct.Catpresas.Commands.DeleteCatpresa;
using CEA.Application.UseCases.IndiArct.Catpresas.Commands.UpdateCatpresa;
using CEA.Application.UseCases.IndiArct.Catpresas.Queries.GetAllCatpresas;
using CEA.Application.UseCases.IndiArct.Catpresas.Queries.GetCatpresaById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.IndiArct
{
    [Route("api/IndiArct/Catpresas")]
    [ApiController]
    public class CatpresasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CatpresasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
            => Ok(await _mediator.Send(new GetAllCatpresasQuery()));

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(int id)
            => Ok(await _mediator.Send(new GetCatpresaByIdQuery(id)));

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateCatpresaCommand command)
            => Ok(await _mediator.Send(command));

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCatpresaCommand command)
            => Ok(await _mediator.Send(command));

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteCatpresaCommand(id)));
    }
}
