using CEA.Application.DTOs.Transparencia;
using CEA.Application.Features.Transparencia.Queries.GetFormatoById;
using CEA.Application.Features.Transparencia.Queries.GetNombreFormato;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Transparencia
{

    public class FormatoController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public FormatoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetFormatoById")]

        public async Task<ActionResult<Result<List<GetFormatoByUserIdDto>>>> GetFormatoById(int userId)
        {
            var result = await _mediator.Send(new GetFormatoByUserIdQuery(userId));
            return new JsonResult(result);
        }

        [HttpGet("GetNombreFormato")]

        public async Task<ActionResult<GetNombreFormatoDto>> GetNombreFormato(string nombreFormato)
        {
            var result = await _mediator.Send(new GetNombreFormatoQuery(nombreFormato));
            return new JsonResult(result);
        }

    }
}
