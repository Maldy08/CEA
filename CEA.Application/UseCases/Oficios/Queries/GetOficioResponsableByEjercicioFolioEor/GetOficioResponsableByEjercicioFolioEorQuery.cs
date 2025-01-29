using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficioResponsableByEjercicioFolioEor
{
    public record GetOficioResponsableByEjercicioFolioEorQuery: IRequest<Result<List<OficioResponsableDto>>>
    {
        public int Ejercicio { get; init; }
        public int Folio { get; init; }
        public int Eor { get; init; }
        public int Rol { get; init; }


        public GetOficioResponsableByEjercicioFolioEorQuery(int ejercicio, int folio, int eor, int rol)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
            Rol = rol;
        }

    }
    internal class GetOficioResponsableByEjercicioFolioEorQueryHandler : IRequestHandler<GetOficioResponsableByEjercicioFolioEorQuery, Result<List<OficioResponsableDto>>>
    {
        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IMapper _mapper;

        public GetOficioResponsableByEjercicioFolioEorQueryHandler(IOficioResponsableRepository oficioResponsableRepository, IMapper mapper)
        {
            _oficioResponsableRepository = oficioResponsableRepository;
            _mapper = mapper;
        }

        public async Task<Result<List<OficioResponsableDto>>> Handle(GetOficioResponsableByEjercicioFolioEorQuery request, CancellationToken cancellationToken)
        {
           var entities = await _oficioResponsableRepository.GetOficioReponsableByEjercicioFolioEor(request.Ejercicio, request.Folio, request.Eor, request.Rol);
           var mappedEntities = _mapper.Map<List<OficioResponsableDto>>(entities);
           return await Result<List<OficioResponsableDto>>.SuccessAsync(mappedEntities);
        }
    }
}
