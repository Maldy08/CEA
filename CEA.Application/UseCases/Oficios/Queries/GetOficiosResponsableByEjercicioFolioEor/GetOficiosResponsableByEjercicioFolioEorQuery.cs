using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosResponsableByEjercicioFolioEor
{
    public record GetOficiosResponsableByEjercicioFolioEorQuery : IRequest<Result<IEnumerable<OficioResponsableDto>>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }


        public GetOficiosResponsableByEjercicioFolioEorQuery(int ejercicio, int folio, int eor)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
        }

        internal class GetOficiosResponsableByEjercicioFolioEorQueryHandler : IRequestHandler<GetOficiosResponsableByEjercicioFolioEorQuery, Result<IEnumerable<OficioResponsableDto>>>
        {

            private readonly IOficioResponsableRepository _oficioResponsableRepository;
            private readonly IMapper _mapper;

            public GetOficiosResponsableByEjercicioFolioEorQueryHandler(IOficioResponsableRepository oficioResponsableRepository, IMapper mapper)
            {
                _oficioResponsableRepository = oficioResponsableRepository;
                _mapper = mapper;
            }

            public async Task<Result<IEnumerable<OficioResponsableDto>>> Handle(GetOficiosResponsableByEjercicioFolioEorQuery request, CancellationToken cancellationToken)
            {
                var entities = await _oficioResponsableRepository.GetOficioReponsableByEjercicioFolioEor(request.Ejercicio, request.Folio, request.Eor);
                var mappedEntities = _mapper.Map<IEnumerable<OficioResponsableDto>>(entities);
        
                var result = await Result<IEnumerable<OficioResponsableDto>>.SuccessAsync(mappedEntities);
                return result;

            }
        }
    }
}
