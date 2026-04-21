using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetAllOficiosByEjercicio
{
    public record GetAllOficiosByEjercicioQuery : IRequest<Result<List<OficioDto>>>
    {
        public int Ejercicio { get; set; }

        public GetAllOficiosByEjercicioQuery(int ejercicio)
        {
            Ejercicio = ejercicio;
        }
    }
    internal class GetAllOficiosByEjercicioQueryHandler : IRequestHandler<GetAllOficiosByEjercicioQuery, Result<List<OficioDto>>>
    {

        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetAllOficiosByEjercicioQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioDto>>> Handle(GetAllOficiosByEjercicioQuery request, CancellationToken cancellationToken)
        {
            var entities = await _oficioRepository.GetAllOficiosByEjercicio(request.Ejercicio);
            var dtos = _mapper.Map<List<OficioDto>>(entities);
            return await Result<List<OficioDto>>.SuccessAsync(dtos);
        }
    }
}
