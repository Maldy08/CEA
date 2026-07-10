using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Bitacora;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CEA.WebAPI.Controllers.Bitacora
{
    [Route("api/Bitacora/Adjuntos")]
    [ApiController]
    [Authorize]
    public class AdjuntosController : ControllerBase
    {
        private readonly IAvanceRepository _avanceRepo;
        private readonly ITemaInvolucradoRepository _involucradoRepo;
        private readonly ITemaRepository _temaRepo;
        private readonly IUserRepository _userRepo;
        private readonly IEmpleadoRepository _empleadoRepo;
        private readonly IWebHostEnvironment _env;

        public AdjuntosController(
            IAvanceRepository avanceRepo,
            ITemaInvolucradoRepository involucradoRepo,
            ITemaRepository temaRepo,
            IUserRepository userRepo,
            IEmpleadoRepository empleadoRepo,
            IWebHostEnvironment env)
        {
            _avanceRepo = avanceRepo;
            _involucradoRepo = involucradoRepo;
            _temaRepo = temaRepo;
            _userRepo = userRepo;
            _empleadoRepo = empleadoRepo;
            _env = env;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Download(int id)
        {
            var userIdClaim = User.FindFirst("userid")?.Value;
            if (!int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var adjunto = await _avanceRepo.GetAdjuntoByIdAsync(id);
            if (adjunto is null) return NotFound();

            var avance = await _avanceRepo.GetByIdAsync(adjunto.IdAvance);
            if (avance is null) return NotFound();

            var permitido = await TieneAccesoAlTemaAsync(userId, avance.IdTema);
            if (!permitido) return Forbid();

            var rutaRelativa = adjunto.Url.TrimStart('/').Replace('/', Path.DirectorySeparatorChar);
            var rutaFisica = Path.Combine(_env.WebRootPath ?? "wwwroot", rutaRelativa);

            if (!System.IO.File.Exists(rutaFisica)) return NotFound();

            var mime = adjunto.TipoMime;
            if (string.IsNullOrWhiteSpace(mime))
            {
                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(rutaFisica, out mime))
                    mime = "application/octet-stream";
            }

            var inline = mime.StartsWith("image/") || mime == "application/pdf";
            var disposition = new System.Net.Mime.ContentDisposition
            {
                FileName = adjunto.Nombre,
                Inline = inline,
            };
            Response.Headers["Content-Disposition"] = disposition.ToString();

            var stream = System.IO.File.OpenRead(rutaFisica);
            return File(stream, mime);
        }

        private async Task<bool> TieneAccesoAlTemaAsync(int userId, int idTema)
        {
            var involucrados = await _involucradoRepo.GetByTemaAsync(idTema);
            if (involucrados.Any(i => i.IdUsuario == userId)) return true;

            var tema = await _temaRepo.GetByIdAsync(idTema);
            if (tema is null) return false;

            var esResponsable = await _empleadoRepo.EsEmpleadoResponsableAsync(userId);
            if (!esResponsable) return false;

            var user = await _userRepo.GetUserByIdEmpleado(userId);
            if (user is null) return false;

            return user.Depto == tema.IdDepartamentoOrigen;
        }
    }
}
