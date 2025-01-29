using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicioAndFolio
{

    public record GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQuery : IRequest<Result<OficioResponsableDto>>
    {
        public int Ejercicio { get; set; }
        public int Folio { get; set; }
        public int Eor { get; set; }
        public int IdEmpleado { get; set; }
        public int Rol { get; set; }


        public GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQuery(int ejercicio, int folio, int eor, int idEmpleado, int rol)
        {
            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
            IdEmpleado = idEmpleado;
            Rol = rol;
        }


    }
    internal class GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQueryHandler : IRequestHandler<GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQuery, Result<OficioResponsableDto>>

    {

        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IMapper _mapper;

        public GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQueryHandler(IOficioResponsableRepository oficioResponsableRepository, IMapper mapper)
        {
            _oficioResponsableRepository = oficioResponsableRepository;
            _mapper = mapper;
        }

        public Task<Result<OficioResponsableDto>> Handle(GetOficioResponsableByEjercicioFolioEorIdEmpleadoRolQuery request, CancellationToken cancellationToken)
        {
            var entity = _oficioResponsableRepository.GetOficioResponsableByEjercicioFolioEorIdEmpleadoRol(request.Ejercicio, request.Folio, request.Eor, request.IdEmpleado, request.Rol);
            return Task.FromResult(Result<OficioResponsableDto>.Success(_mapper.Map<OficioResponsableDto>(entity)));
        }
    }
}
