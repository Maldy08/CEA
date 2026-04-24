using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Queries.GetAllTemas
{
    public record GetAllTemasQuery : IRequest<Result<IEnumerable<TemaDto>>>;

    internal class GetAllTemasQueryHandler : IRequestHandler<GetAllTemasQuery, Result<IEnumerable<TemaDto>>>
    {
        private readonly ITemaRepository _temaRepository;

        public GetAllTemasQueryHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<IEnumerable<TemaDto>>> Handle(GetAllTemasQuery request, CancellationToken cancellationToken)
        {
            var temas = await _temaRepository.GetAllConContadoresAsync();
            return Result<IEnumerable<TemaDto>>.Success(temas);
        }
    }
}
