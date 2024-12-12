using CEA.Application.UseCases.Oficios.Queries.GetOficioParametroByEjercicio;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Oficios
{
    [Route("api/OficioParametro")]
    [ApiController]
    public class OficioParametroController : ControllerBase
    {

        private readonly IMediator _mediator;

        public OficioParametroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetOficioParametroByEjercicio(int ejercicio)
        {
            var result = await _mediator.Send(new GetOficioParametroByEjercicioQuery(ejercicio));
            return Ok(result);
        }
    }
}
