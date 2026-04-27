using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Avances.Commands.UpdateAvance
{
    public record UpdateAvanceCommand(int Id, string Observaciones) : IRequest<Result<int>>;

    internal class UpdateAvanceCommandHandler : IRequestHandler<UpdateAvanceCommand, Result<int>>
    {
        private readonly IAvanceRepository _avanceRepository;

        public UpdateAvanceCommandHandler(IAvanceRepository avanceRepository)
        {
            _avanceRepository = avanceRepository;
        }

        public async Task<Result<int>> Handle(UpdateAvanceCommand request, CancellationToken cancellationToken)
        {
            await _avanceRepository.UpdateObservacionesAsync(request.Id, request.Observaciones);
            return Result<int>.Success(request.Id);
        }
    }
}
