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
        public List<AdjuntoInfo>? Adjuntos { get; init; }
    }

    internal class CreateAvanceCommandHandler : IRequestHandler<CreateAvanceCommand, Result<int>>
    {
        private readonly IAvanceRepository _avanceRepository;

        public CreateAvanceCommandHandler(IAvanceRepository avanceRepository)
        {
            _avanceRepository = avanceRepository;
        }

        public async Task<Result<int>> Handle(CreateAvanceCommand request, CancellationToken cancellationToken)
        {
            var avance = new Avance
            {
                IdTema = request.IdTema,
                IdUsuario = request.IdUsuario,
                Observaciones = request.Observaciones,
                FechaHora = DateTime.Now
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

            return Result<int>.Success(created.Id);
        }
    }
}
