using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.DeletePresaNivel
{
    public record DeletePresaNivelCommand(int Id) : IRequest<Result<int>>;

    internal class DeletePresaNivelCommandHandler : IRequestHandler<DeletePresaNivelCommand, Result<int>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public DeletePresaNivelCommandHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(DeletePresaNivelCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Result<int>.Success(request.Id);
        }
    }
}
