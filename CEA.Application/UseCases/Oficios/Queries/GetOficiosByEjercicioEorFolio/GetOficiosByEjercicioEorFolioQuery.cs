

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.Oficios.Queries.GetOficiosByEjercicioEorFolio
{
    public record GetOficiosByEjercicioEorFolioQuery : IRequest<Result<OficioDto>>
    {
        public int Ejercicio { get; set; }
        public int Eor { get; set; }
        public int Folio { get; set; }

        public GetOficiosByEjercicioEorFolioQuery(int ejercicio, int eor, int folio)
        {
            Ejercicio = ejercicio;
            Eor = eor;
            Folio = folio;
        }

    }
    internal class GetOficiosByEjercicioEorFolioQueryHandler : IRequestHandler<GetOficiosByEjercicioEorFolioQuery, Result<OficioDto>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IMapper _mapper;

        public GetOficiosByEjercicioEorFolioQueryHandler(IOficioRepository oficioRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _mapper = mapper;
        }

        public async Task<Result<OficioDto>> Handle(GetOficiosByEjercicioEorFolioQuery request, CancellationToken cancellationToken)
        {
            var oficio = await _oficioRepository.GetOficioByFolio(request.Ejercicio, request.Eor, request.Folio);
            var oficioDto = _mapper.Map<OficioDto>(oficio);
            return await Result<OficioDto>.SuccessAsync(oficioDto);
        }
    }
}
