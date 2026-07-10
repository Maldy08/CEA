using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresasNivelesByPresa
{
    public record GetPresasNivelesByPresaQuery(int IdPresa) : IRequest<Result<IEnumerable<IndiArctPresasNivelesDto>>>;

    internal class GetPresasNivelesByPresaQueryHandler : IRequestHandler<GetPresasNivelesByPresaQuery, Result<IEnumerable<IndiArctPresasNivelesDto>>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;

        public GetPresasNivelesByPresaQueryHandler(IIndiArctPresasNivelesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<IndiArctPresasNivelesDto>>> Handle(GetPresasNivelesByPresaQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetByPresaAsync(request.IdPresa);
            return Result<IEnumerable<IndiArctPresasNivelesDto>>.Success(data);
        }
    }
}
