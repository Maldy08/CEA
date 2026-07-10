using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresasGrid
{
    // Devuelve TODAS las presas del catálogo con su volumen del año/mes dado
    // (null si aún no se ha capturado). Llena el formulario de un solo golpe.
    public record GetPresasGridQuery(int Anio, int Mes) : IRequest<Result<IEnumerable<PresaCapturaDto>>>;

    internal class GetPresasGridQueryHandler : IRequestHandler<GetPresasGridQuery, Result<IEnumerable<PresaCapturaDto>>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public GetPresasGridQueryHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<PresaCapturaDto>>> Handle(GetPresasGridQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetGridAsync(request.Anio, request.Mes);
            return Result<IEnumerable<PresaCapturaDto>>.Success(data);
        }
    }
}
