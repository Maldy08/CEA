using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Queries.GetEnergiaGrid
{
    // Devuelve los 12 meses del año con sus valores de energía (null si no se han capturado).
    public record GetEnergiaGridQuery(int Anio) : IRequest<Result<IEnumerable<EnergiaCapturaDto>>>;

    internal class GetEnergiaGridQueryHandler : IRequestHandler<GetEnergiaGridQuery, Result<IEnumerable<EnergiaCapturaDto>>>
    {
        private readonly IIndiArctEnergiaRepository _repository;

        public GetEnergiaGridQueryHandler(IIndiArctEnergiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<EnergiaCapturaDto>>> Handle(GetEnergiaGridQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetGridAsync(request.Anio);
            return Result<IEnumerable<EnergiaCapturaDto>>.Success(data);
        }
    }
}
