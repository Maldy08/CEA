using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Catpresas.Queries.GetAllCatpresas
{
    public record GetAllCatpresasQuery : IRequest<Result<IEnumerable<IndiArctCatpresasDto>>>;

    internal class GetAllCatpresasQueryHandler : IRequestHandler<GetAllCatpresasQuery, Result<IEnumerable<IndiArctCatpresasDto>>>
    {
        private readonly IIndiArctCatpresasRepository _repository;

        public GetAllCatpresasQueryHandler(IIndiArctCatpresasRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<IndiArctCatpresasDto>>> Handle(GetAllCatpresasQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync();
            return Result<IEnumerable<IndiArctCatpresasDto>>.Success(data);
        }
    }
}
