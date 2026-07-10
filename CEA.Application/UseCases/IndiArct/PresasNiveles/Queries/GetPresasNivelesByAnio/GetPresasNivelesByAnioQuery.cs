using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresasNivelesByAnio
{
    // Todas las capturas de volumen de un año (para la consulta/reporte anual).
    public record GetPresasNivelesByAnioQuery(int Anio) : IRequest<Result<IEnumerable<IndiArctPresasNivelesDto>>>;

    internal class GetPresasNivelesByAnioQueryHandler : IRequestHandler<GetPresasNivelesByAnioQuery, Result<IEnumerable<IndiArctPresasNivelesDto>>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public GetPresasNivelesByAnioQueryHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<IndiArctPresasNivelesDto>>> Handle(GetPresasNivelesByAnioQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByAnioAsync(request.Anio);
            return Result<IEnumerable<IndiArctPresasNivelesDto>>.Success(data);
        }
    }
}
