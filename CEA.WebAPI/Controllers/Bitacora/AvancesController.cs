using CEA.Application.UseCases.Bitacora.Avances.Commands.CreateAvance;
using CEA.Application.UseCases.Bitacora.Avances.Commands.DeleteAvance;
using CEA.Application.UseCases.Bitacora.Avances.Queries.GetAvancesByTema;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CEA.WebAPI.Controllers.Bitacora
{
    [Route("api/Bitacora/Avances")]
    [ApiController]
    public class AvancesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;

        public AvancesController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpGet("GetByTema/{id}")]
        public async Task<IActionResult> GetByTema(int id)
            => Ok(await _mediator.Send(new GetAvancesByTemaQuery(id)));

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] int idTema, [FromForm] int idUsuario, [FromForm] string observaciones, IList<IFormFile>? adjuntos)
        {
            var adjuntosInfo = new List<AdjuntoInfo>();

            if (adjuntos != null && adjuntos.Count > 0)
            {
                var carpeta = Path.Combine(_env.WebRootPath ?? "wwwroot", "bitacora", "adjuntos");
                Directory.CreateDirectory(carpeta);

                foreach (var file in adjuntos)
                {
                    var nombreArchivo = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                    var rutaCompleta = Path.Combine(carpeta, nombreArchivo);
                    using var stream = new FileStream(rutaCompleta, FileMode.Create);
                    await file.CopyToAsync(stream);
                    adjuntosInfo.Add(new AdjuntoInfo
                    {
                        Nombre = file.FileName,
                        Url = $"/bitacora/adjuntos/{nombreArchivo}",
                        TipoMime = file.ContentType
                    });
                }
            }

            var command = new CreateAvanceCommand
            {
                IdTema = idTema,
                IdUsuario = idUsuario,
                Observaciones = observaciones,
                Adjuntos = adjuntosInfo
            };

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
            => Ok(await _mediator.Send(new DeleteAvanceCommand(id)));
    }
}
