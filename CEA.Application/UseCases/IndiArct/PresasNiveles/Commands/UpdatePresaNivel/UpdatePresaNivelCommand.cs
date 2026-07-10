using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Commands.UpdatePresaNivel
{
    public record UpdatePresaNivelCommand : IRequest<Result<int>>
    {
        public int Id { get; init; }
        public int Anio { get; init; }
        public int Mes { get; init; }
        public int IdPresa { get; init; }
        public decimal VolumenM3 { get; init; }
    }

    internal class UpdatePresaNivelCommandHandler : IRequestHandler<UpdatePresaNivelCommand, Result<int>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public UpdatePresaNivelCommandHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdatePresaNivelCommand request, CancellationToken cancellationToken)
        {
            var entity = new IndiArctPresasNiveles
            {
                Id = request.Id,
                Anio = request.Anio,
                Mes = request.Mes,
                IdPresa = request.IdPresa,
                VolumenM3 = request.VolumenM3
            };
            await _repository.UpdateAsync(entity);
            return Result<int>.Success(request.Id);
        }
    }
}
