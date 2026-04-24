using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Involucrados.Commands.RemoveInvolucrado
{
    public record RemoveInvolucradoCommand(int IdTema, int IdUsuario) : IRequest<Result<int>>;

    internal class RemoveInvolucradoCommandHandler : IRequestHandler<RemoveInvolucradoCommand, Result<int>>
    {
        private readonly ITemaInvolucradoRepository _involucradoRepository;

        public RemoveInvolucradoCommandHandler(ITemaInvolucradoRepository involucradoRepository)
        {
            _involucradoRepository = involucradoRepository;
        }

        public async Task<Result<int>> Handle(RemoveInvolucradoCommand request, CancellationToken cancellationToken)
        {
            await _involucradoRepository.RemoveAsync(request.IdTema, request.IdUsuario);
            return Result<int>.Success(request.IdTema);
        }
    }
}
