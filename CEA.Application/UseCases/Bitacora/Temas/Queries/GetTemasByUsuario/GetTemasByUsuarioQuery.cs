using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Queries.GetTemasByUsuario
{
    public record GetTemasByUsuarioQuery(int IdUsuario) : IRequest<Result<IEnumerable<TemaDto>>>;

    internal class GetTemasByUsuarioQueryHandler : IRequestHandler<GetTemasByUsuarioQuery, Result<IEnumerable<TemaDto>>>
    {
        private readonly ITemaRepository _temaRepository;

        public GetTemasByUsuarioQueryHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<IEnumerable<TemaDto>>> Handle(GetTemasByUsuarioQuery request, CancellationToken cancellationToken)
        {
            var temas = await _temaRepository.GetByUsuarioConContadoresAsync(request.IdUsuario);
            return Result<IEnumerable<TemaDto>>.Success(temas);
        }
    }
}
