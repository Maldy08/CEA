using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Queries.GetAllEnergia
{
    public record GetAllEnergiaQuery : IRequest<Result<IEnumerable<IndiArctEnergiaDto>>>;

    internal class GetAllEnergiaQueryHandler : IRequestHandler<GetAllEnergiaQuery, Result<IEnumerable<IndiArctEnergiaDto>>>
    {
        private readonly IIndiArctEnergiaRepository _repository;

        public GetAllEnergiaQueryHandler(IIndiArctEnergiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<IndiArctEnergiaDto>>> Handle(GetAllEnergiaQuery request, CancellationToken cancellationToken)
        {
            var data = await _repository.GetAllAsync();
            return Result<IEnumerable<IndiArctEnergiaDto>>.Success(data);
        }
    }
}
