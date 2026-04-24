using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Avances.Commands.DeleteAvance
{
    public record DeleteAvanceCommand(int Id) : IRequest<Result<int>>;

    internal class DeleteAvanceCommandHandler : IRequestHandler<DeleteAvanceCommand, Result<int>>
    {
        private readonly IAvanceRepository _avanceRepository;

        public DeleteAvanceCommandHandler(IAvanceRepository avanceRepository)
        {
            _avanceRepository = avanceRepository;
        }

        public async Task<Result<int>> Handle(DeleteAvanceCommand request, CancellationToken cancellationToken)
        {
            await _avanceRepository.DeleteAsync(request.Id);
            return Result<int>.Success(request.Id);
        }
    }
}
