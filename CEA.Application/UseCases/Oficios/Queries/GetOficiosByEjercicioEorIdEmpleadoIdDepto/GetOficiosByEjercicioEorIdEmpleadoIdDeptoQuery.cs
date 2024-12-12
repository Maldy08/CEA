

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetOficiosByEjercicioEorIdEmpleadoIdDepto
{
    public record GetOficiosByEjercicioEorIdEmpleadoIdDeptoQuery : IRequest<Result<List<OficioDto>>>
    {
        public int Ejercicio { get; set; }
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }
        public int IdDepto { get; set; }

        public GetOficiosByEjercicioEorIdEmpleadoIdDeptoQuery(int ejercicio, int eor, int idEmpleado, int idDepto)
        {
            Ejercicio = ejercicio;
            Eor = eor;
            IdEmpleado = idEmpleado;
            IdDepto = idDepto;
        }
    }
    internal class GetOficiosByEjercicioEorIdEmpleadoIdDeptoQueryHandler : IRequestHandler<GetOficiosByEjercicioEorIdEmpleadoIdDeptoQuery, Result<List<OficioDto>>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetOficiosByEjercicioEorIdEmpleadoIdDeptoQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioDto>>> Handle(GetOficiosByEjercicioEorIdEmpleadoIdDeptoQuery request, CancellationToken cancellationToken)
        {
            var oficios = await _oficioRepository.GetOficiosUsuarios(request.Ejercicio, request.Eor, request.IdEmpleado, request.IdDepto);
            var oficiosDto = _mapper.Map<List<OficioDto>>(oficios);
            return await Result<List<OficioDto>>.SuccessAsync(oficiosDto); 
        }
    }
}
