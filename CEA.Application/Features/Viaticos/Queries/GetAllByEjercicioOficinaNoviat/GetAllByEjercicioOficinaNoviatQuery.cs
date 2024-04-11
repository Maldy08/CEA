using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.Viaticos.Queries.GetAllByEjercicioOficinaNoviat
{

    public record GetAllByEjercicioOficinaNoviatQuery : IRequest<Result<GetAllViaticosDto>>
    {
        public int Ejercicio { get; set; }
        public int Oficina { get; set; }
        public int Noviat { get; set; }

        public GetAllByEjercicioOficinaNoviatQuery(int ejercicio, int oficina, int noviat)
        {
            Ejercicio = ejercicio;
            Oficina = oficina;
            Noviat = noviat;
        }
    }
    internal class GetAllByEjercicioOficinaNoviatHandler : IRequestHandler<GetAllByEjercicioOficinaNoviatQuery, Result<GetAllViaticosDto>>
    {
        private readonly IMapper _mapper;
        private readonly IViaticoRepository _viaticoRepository;

        public GetAllByEjercicioOficinaNoviatHandler(IMapper mapper, IViaticoRepository viaticoRepository)
        {
            _mapper = mapper;
            _viaticoRepository = viaticoRepository;
        }
        public async Task<Result<GetAllViaticosDto>> Handle(GetAllByEjercicioOficinaNoviatQuery request, CancellationToken cancellationToken)
        {
           var entities = await _viaticoRepository.GetAllByEjercicioOficinaNoviat(request.Ejercicio, request.Oficina, request.Noviat);
            var viaticos = _mapper.Map<GetAllViaticosDto>(entities);
            return await Result<GetAllViaticosDto>.SuccessAsync(viaticos);
        }
    }
}
