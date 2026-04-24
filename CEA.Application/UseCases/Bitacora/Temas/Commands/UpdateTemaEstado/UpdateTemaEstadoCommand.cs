using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Commands.UpdateTemaEstado
{
    public record UpdateTemaEstadoCommand(int Id, string Estado) : IRequest<Result<int>>;

    internal class UpdateTemaEstadoCommandHandler : IRequestHandler<UpdateTemaEstadoCommand, Result<int>>
    {
        private readonly ITemaRepository _temaRepository;

        public UpdateTemaEstadoCommandHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<int>> Handle(UpdateTemaEstadoCommand request, CancellationToken cancellationToken)
        {
            await _temaRepository.UpdateEstadoAsync(request.Id, request.Estado);
            return Result<int>.Success(request.Id);
        }
    }
}
