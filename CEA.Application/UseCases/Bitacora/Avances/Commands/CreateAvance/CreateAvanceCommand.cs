using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Domain.Entities.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Avances.Commands.CreateAvance
{
    public class AdjuntoInfo
    {
        public string Nombre { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? TipoMime { get; set; }
    }

    public record CreateAvanceCommand : IRequest<Result<int>>
    {
        public int IdTema { get; init; }
        public int IdUsuario { get; init; }
        public string Observaciones { get; init; } = string.Empty;
        public string? Estado { get; init; }
        public int? IdAvancePadre { get; init; }
        public List<AdjuntoInfo>? Adjuntos { get; init; }
    }

    internal class CreateAvanceCommandHandler : IRequestHandler<CreateAvanceCommand, Result<int>>
    {
        private readonly IAvanceRepository _avanceRepository;
        private readonly ITemaRepository _temaRepository;

        public CreateAvanceCommandHandler(IAvanceRepository avanceRepository, ITemaRepository temaRepository)
        {
            _avanceRepository = avanceRepository;
            _temaRepository = temaRepository;
        }

        public async Task<Result<int>> Handle(CreateAvanceCommand request, CancellationToken cancellationToken)
        {
            if (request.IdAvancePadre.HasValue)
            {
                var padre = await _avanceRepository.GetByIdAsync(request.IdAvancePadre.Value);
                if (padre == null || padre.IdTema != request.IdTema)
                {
                    return Result<int>.Failure("La entrada a la que se intenta responder no existe en este tema.");
                }
                if (padre.IdAvancePadre.HasValue)
                {
                    return Result<int>.Failure("Solo se puede responder a entradas raíz; no se permiten respuestas anidadas.");
                }
                if (padre.IdUsuario == request.IdUsuario)
                {
                    return Result<int>.Failure("No puedes responder a tus propias entradas.");
                }
            }

            var tema = await _temaRepository.GetByIdAsync(request.IdTema);
            var estadoFinal = string.IsNullOrWhiteSpace(request.Estado)
                ? (tema?.Estado ?? "Pendiente")
                : request.Estado!;

            var avance = new Avance
            {
                IdTema = request.IdTema,
                IdUsuario = request.IdUsuario,
                Observaciones = request.Observaciones,
                Estado = estadoFinal,
                FechaHora = DateTime.Now,
                IdAvancePadre = request.IdAvancePadre
            };
            var created = await _avanceRepository.AddAsync(avance);

            if (request.Adjuntos != null)
            {
                foreach (var info in request.Adjuntos)
                {
                    var adjunto = new Adjunto
                    {
                        IdAvance = created.Id,
                        Nombre = info.Nombre,
                        Url = info.Url,
                        TipoMime = info.TipoMime
                    };
                    await _avanceRepository.AddAdjuntoAsync(adjunto);
                }
            }

            if (tema != null && tema.Estado != estadoFinal)
            {
                await _temaRepository.UpdateEstadoAsync(tema.Id, estadoFinal);
            }

            return Result<int>.Success(created.Id);
        }
    }
}
