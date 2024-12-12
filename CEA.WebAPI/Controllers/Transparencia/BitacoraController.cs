
using CEA.Application.DTOs.Transparencia;
using CEA.Application.Features.Transparencia.Commands.CreateBitacora;
using CEA.Application.Features.Transparencia.Commands.DeleteBitacora;
using CEA.Application.Features.Transparencia.Commands.UpdateBitacora;
using CEA.Application.Features.Transparencia.Queries.GetBitacorasByUserId;
using CEA.Application.Features.Transparencia.Queries.GetBitacorasByUserIdAndFormato;
using CEA.Shared.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Transparencia
{
    [Route("api/Transparencia/Bitacoras")]
    [ApiController]
    public class BitacoraController : ApiControllerBaseTransparencia
    {
        private readonly IMediator _mediator;

        public BitacoraController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("GetBitacorasByUserId")]
        public async Task<ActionResult<Result<List<GetBitacorasByUserIdDto>>>> GetBitacorasByUserId(int userId)
        {
            var result = await _mediator.Send(new GetBitacorasByUserIdQuery(userId));
            return new JsonResult(result);
        }

        [HttpGet("GetBitacorasByUserIdAndFormato")]
        public async Task<ActionResult<Result<List<GetBitacorasByUserIdDto>>>> GetBitacorasByUserIdAndFormato(int userId, string formato)
        {
            var result = await _mediator.Send(new GetBitacorasByUserIdAndFormatoQuery(userId,formato));
            return new JsonResult(result);
        }

        [HttpPost("CreateBitacora")]
        public async Task<ActionResult<Result<List<BitacoraArchivoDto>>>> CreateBitacora([FromForm] BitacoraArchivoDto bitacora)
        {
            var command = new CreateBitacoraCommand()
            {
                idBitacora = bitacora.idBitacora,
                codigo = bitacora.codigo,
                idUsuario = bitacora.idUsuario,
                trimestre = bitacora.trimestre,
                periodo = bitacora.periodo,
                archivos = bitacora.archivos,
            };

            var result = await _mediator.Send(command);
            return new JsonResult(result);
            //1return new JsonResult(await _mediator.Send(command));
            //CreateBitacoraCommand command
            // return await _mediator.Send(command);
        }

        [HttpPost("DeleteBitacora/{id}")]
        public async Task<ActionResult<Result<int>>> DeleteBitacora(int id)
        {
            var result = await _mediator.Send(new DeleteBitacoraCommand(id));
            return new JsonResult(result);
        }


        [HttpPut("UpdateBitacora")]
        public async Task<ActionResult<Result<int>>> UpdateBitacora([FromForm] BitacoraArchivoDto bitacora)
        {
            var command = new UpdateBitacoraCommand()
            {
                IdBitacora = bitacora.idBitacora,
                codigo = bitacora.codigo,
                idUsuario = bitacora.idUsuario,
                trimestre = bitacora.trimestre,
                periodo = bitacora.periodo,
                Archivos = bitacora.archivos,
            };

            return new JsonResult(await _mediator.Send(command));
        }
    }
}


