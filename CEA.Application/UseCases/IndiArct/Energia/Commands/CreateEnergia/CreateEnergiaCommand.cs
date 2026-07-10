using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Domain.Entities.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Commands.CreateEnergia
{
    public record CreateEnergiaCommand : IRequest<Result<int>>
    {
        public int Anio { get; init; }
        public int Mes { get; init; }
        public decimal Volumenes { get; init; }
        public decimal Kwh { get; init; }
        public decimal Costo { get; init; }
    }

    internal class CreateEnergiaCommandHandler : IRequestHandler<CreateEnergiaCommand, Result<int>>
    {
        private readonly IIndiArctEnergiaRepository _repository;

        public CreateEnergiaCommandHandler(IIndiArctEnergiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<int>> Handle(CreateEnergiaCommand request, CancellationToken cancellationToken)
        {
            var entity = new IndiArctEnergia
            {
                Anio = request.Anio,
                Mes = request.Mes,
                Volumenes = request.Volumenes,
                Kwh = request.Kwh,
                Costo = request.Costo
            };
            var created = await _repository.AddAsync(entity);
            return Result<int>.Success(created.Id);
        }
    }
}
