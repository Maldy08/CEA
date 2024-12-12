

using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Viaticos.Queries.GetDetalleViaticoByEjercicioNoviatOficina
{
    public class GetDetalleViaticoByEjercicioNoviatOficinaQuery : IRequest<Result<ViaticoDetalleDto>>
    {
        public int Ejercicio { get; set; }
        public int Noviat { get; set; }
        public int Oficina { get; set; }

        public GetDetalleViaticoByEjercicioNoviatOficinaQuery(int ejercicio, int noviat, int oficina)
        {
            Ejercicio = ejercicio;
            Noviat = noviat;
            Oficina = oficina;
        }
    }
    internal class GetDetalleViaticoByEjercicioNoviatOficinaQueryHandler : IRequestHandler<GetDetalleViaticoByEjercicioNoviatOficinaQuery, Result<ViaticoDetalleDto>>
    {
        private readonly IViaticoDetalleRepository _viaticoDetalleRepository;
        private readonly IMapper _mapper;

        public GetDetalleViaticoByEjercicioNoviatOficinaQueryHandler(IViaticoDetalleRepository viaticoDetalleRepository, IMapper mapper)
        {
            _viaticoDetalleRepository = viaticoDetalleRepository;
            _mapper = mapper;
        }

        public async Task<Result<ViaticoDetalleDto>> Handle(GetDetalleViaticoByEjercicioNoviatOficinaQuery request, CancellationToken cancellationToken)
        {
           var entities = await _viaticoDetalleRepository.GetAllViaticosByEjercicioAndNoviatAndOficina(request.Ejercicio, request.Noviat, request.Oficina);
            var viaticos = _mapper.Map<ViaticoDetalleDto>(entities);
            return await Result<ViaticoDetalleDto>.SuccessAsync(viaticos);
        }
    }
}
