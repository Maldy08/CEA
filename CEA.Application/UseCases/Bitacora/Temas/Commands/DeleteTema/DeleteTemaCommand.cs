using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Commands.DeleteTema
{
    public record DeleteTemaCommand(int Id) : IRequest<Result<int>>;

    internal class DeleteTemaCommandHandler : IRequestHandler<DeleteTemaCommand, Result<int>>
    {
        private readonly ITemaRepository _temaRepository;

        public DeleteTemaCommandHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<int>> Handle(DeleteTemaCommand request, CancellationToken cancellationToken)
        {
            await _temaRepository.DeleteAsync(request.Id);
            return Result<int>.Success(request.Id);
        }
    }
}
