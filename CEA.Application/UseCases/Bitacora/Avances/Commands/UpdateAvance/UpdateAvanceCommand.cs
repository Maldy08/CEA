using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Avances.Commands.UpdateAvance
{
    public record UpdateAvanceCommand(int Id, string Observaciones, string? Estado) : IRequest<Result<int>>;

    internal class UpdateAvanceCommandHandler : IRequestHandler<UpdateAvanceCommand, Result<int>>
    {
        private readonly IAvanceRepository _avanceRepository;
        private readonly ITemaRepository _temaRepository;

        public UpdateAvanceCommandHandler(IAvanceRepository avanceRepository, ITemaRepository temaRepository)
        {
            _avanceRepository = avanceRepository;
            _temaRepository = temaRepository;
        }

        public async Task<Result<int>> Handle(UpdateAvanceCommand request, CancellationToken cancellationToken)
        {
            var existing = await _avanceRepository.GetByIdAsync(request.Id);
            if (existing == null) return Result<int>.Success(request.Id);

            var estadoFinal = string.IsNullOrWhiteSpace(request.Estado) ? existing.Estado : request.Estado!;

            await _avanceRepository.UpdateAsync(request.Id, request.Observaciones, estadoFinal);

            var ultimo = await _avanceRepository.GetUltimoByTemaAsync(existing.IdTema);
            if (ultimo != null && ultimo.Id == request.Id)
            {
                var tema = await _temaRepository.GetByIdAsync(existing.IdTema);
                if (tema != null && tema.Estado != estadoFinal)
                {
                    await _temaRepository.UpdateEstadoAsync(tema.Id, estadoFinal);
                }
            }

            return Result<int>.Success(request.Id);
        }
    }
}
