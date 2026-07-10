using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Catpresas.Commands.DeleteCatpresa
{
    public record DeleteCatpresaCommand(int Id) : IRequest<Result<int>>;

    internal class DeleteCatpresaCommandHandler : IRequestHandler<DeleteCatpresaCommand, Result<int>>
    {
        private readonly IIndiArctCatpresasRepository _repository;

        public DeleteCatpresaCommandHandler(IIndiArctCatpresasRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(DeleteCatpresaCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Result<int>.Success(request.Id);
        }
    }
}
