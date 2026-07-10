using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Commands.UpdateEnergia
{
    public record UpdateEnergiaCommand : IRequest<Result<int>>
    {
        public int Id { get; init; }
        public int Anio { get; init; }
        public int Mes { get; init; }
        public decimal Volumenes { get; init; }
        public decimal Kwh { get; init; }
        public decimal Costo { get; init; }
    }

    internal class UpdateEnergiaCommandHandler : IRequestHandler<UpdateEnergiaCommand, Result<int>>
    {
        private readonly IIndiArctEnergiaRepository _repository;

        public UpdateEnergiaCommandHandler(IIndiArctEnergiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(UpdateEnergiaCommand request, CancellationToken cancellationToken)
        {
            var entity = new IndiArctEnergia
            {
                Id = request.Id,
                Anio = request.Anio,
                Mes = request.Mes,
                Volumenes = request.Volumenes,
                Kwh = request.Kwh,
                Costo = request.Costo
            };
            await _repository.UpdateAsync(entity);
            return Result<int>.Success(request.Id);
        }
    }
}
