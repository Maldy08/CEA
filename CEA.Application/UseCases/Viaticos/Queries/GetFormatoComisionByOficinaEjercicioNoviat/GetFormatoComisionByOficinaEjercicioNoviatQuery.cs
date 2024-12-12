

using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetFormatoComisionByOficinaEjercicioNoviat
{
    public record GetFormatoComisionByOficinaEjercicioNoviatQuery : IRequest<Result<FormatoComisionDto>>
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }

        public GetFormatoComisionByOficinaEjercicioNoviatQuery(int oficina, int ejercicio, int noViat)
        {
            Oficina = oficina;
            Ejercicio = ejercicio;
            NoViat = noViat;
        }
    }
    internal class GetFormatoComisionByOficinaEjercicioNoviatHandler : IRequestHandler<GetFormatoComisionByOficinaEjercicioNoviatQuery, Result<FormatoComisionDto>>
    {
        private readonly IFormatoComisionRepository _formatoComision;

        private readonly IMapper _mapper;

        public GetFormatoComisionByOficinaEjercicioNoviatHandler(IFormatoComisionRepository formatoComision, IMapper mapper)
        {
            _formatoComision = formatoComision;
            _mapper = mapper;
        }
        public async Task<Result<FormatoComisionDto>> Handle(GetFormatoComisionByOficinaEjercicioNoviatQuery request, CancellationToken cancellationToken)
        {
            var entity = await _formatoComision.GetFormatoComisionByOficinaEjercicioNoviat(request.Oficina, request.Ejercicio, request.NoViat);
            var formatoComision = _mapper.Map<FormatoComisionDto>(entity);
            return await Result<FormatoComisionDto>.SuccessAsync(formatoComision);
        }
    }
}
