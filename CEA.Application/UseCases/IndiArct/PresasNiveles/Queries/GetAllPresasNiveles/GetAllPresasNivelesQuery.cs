using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetAllPresasNiveles
{
    public record GetAllPresasNivelesQuery : IRequest<Result<IEnumerable<IndiArctPresasNivelesDto>>>;

    internal class GetAllPresasNivelesQueryHandler : IRequestHandler<GetAllPresasNivelesQuery, Result<IEnumerable<IndiArctPresasNivelesDto>>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public GetAllPresasNivelesQueryHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<IndiArctPresasNivelesDto>>> Handle(GetAllPresasNivelesQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync();
            return Result<IEnumerable<IndiArctPresasNivelesDto>>.Success(data);
        }
    }
}
