using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.CreatePresaNivel
{
    public record CreatePresaNivelCommand : IRequest<Result<int>>
    {
        public int Anio { get; init; }
        public int Mes { get; init; }
        public int IdPresa { get; init; }
        public decimal VolumenM3 { get; init; }
    }

    internal class CreatePresaNivelCommandHandler : IRequestHandler<CreatePresaNivelCommand, Result<int>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public CreatePresaNivelCommandHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreatePresaNivelCommand request, CancellationToken cancellationToken)
        {
            var entity = new IndiArctPresasNiveles
            {
                Anio = request.Anio,
                Mes = request.Mes,
                IdPresa = request.IdPresa,
                VolumenM3 = request.VolumenM3
            };
            var created = await _repository.AddAsync(entity);
            return Result<int>.Success(created.Id);
        }
    }
}
