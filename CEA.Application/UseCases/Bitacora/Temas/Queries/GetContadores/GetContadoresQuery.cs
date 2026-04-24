using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Queries.GetContadores
{
    public record GetContadoresQuery : IRequest<Result<ContadoresDto>>;

    internal class GetContadoresQueryHandler : IRequestHandler<GetContadoresQuery, Result<ContadoresDto>>
    {
        private readonly ITemaRepository _temaRepository;

        public GetContadoresQueryHandler(ITemaRepository temaRepository)
        {
            _temaRepository = temaRepository;
        }

        public async Task<Result<ContadoresDto>> Handle(GetContadoresQuery request, CancellationToken cancellationToken)
        {
            var temas = await _temaRepository.GetAllAsync();
            var lista = temas.ToList();
            var contadores = new ContadoresDto
            {
                TemasPendientes = lista.Count(t => t.Estado == "Pendiente"),
                TemasActivos = lista.Count(t => t.Estado == "Activo"),
                TemasPausados = lista.Count(t => t.Estado == "Pausado"),
                TemasCompletados = lista.Count(t => t.Estado == "Completado"),
                TotalTemas = lista.Count
            };
            return Result<ContadoresDto>.Success(contadores);
        }
    }
}
