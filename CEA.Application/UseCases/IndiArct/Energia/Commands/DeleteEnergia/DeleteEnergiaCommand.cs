using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Commands.DeleteEnergia
{
    public record DeleteEnergiaCommand(int Id) : IRequest<Result<int>>;

    internal class DeleteEnergiaCommandHandler : IRequestHandler<DeleteEnergiaCommand, Result<int>>
    {
        private readonly IIndiArctEnergiaRepository _repository;

        public DeleteEnergiaCommandHandler(IIndiArctEnergiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(DeleteEnergiaCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Result<int>.Success(request.Id);
        }
    }
}
