

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Features.Oficios.Queries.GetOficiosBitacoraByEjercicioFolioEor;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Application.UseCases.GetUserByIdEmpleado;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicio
{

    public record GetOficiosMcByEorAndEjercicioQuery : IRequest<Result<List<OficioDto>>>
    {
        public int Eor { get; set; }
        public int Ejercicio { get; set; }

        public GetOficiosMcByEorAndEjercicioQuery(int eor, int ejercicio)
        {
            Eor = eor;
            Ejercicio = ejercicio;
        }
    }
    internal class GetOficiosMcByEorAndEjercicioQueryHandler : IRequestHandler<GetOficiosMcByEorAndEjercicioQuery, Result<List<OficioDto>>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IOficioBitacoraRepository _oficioBitacoraRepository;
        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;


        public GetOficiosMcByEorAndEjercicioQueryHandler(IOficioRepository oficioRepository, IOficioBitacoraRepository oficioBitacoraRepository, IOficioResponsableRepository oficioResponsableRepository, IMapper mapper, IMediator mediator)
        {
            _oficioRepository = oficioRepository;
            _oficioBitacoraRepository = oficioBitacoraRepository;
            _oficioResponsableRepository = oficioResponsableRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<Result<List<OficioDto>>> Handle(GetOficiosMcByEorAndEjercicioQuery request, CancellationToken cancellationToken)
        {

            var oficios = await _oficioRepository.GetOficiosMCByEjercicio(request.Eor, request.Ejercicio);
            
            foreach (var oficio in oficios)
            {
                var bitacoras = await _mediator.Send(new GetOficiosBitacoraByEjercicioFolioEorQuery(oficio.Ejercicio, oficio.Folio, oficio.Eor));
                if (bitacoras.Data != null)
                {
                    foreach (var bitacora in bitacoras.Data)
                    {

                        oficio.OficioBitacora.Add(new OficioBitacoraDto
                        {
                            Comentarios = bitacora.Comentarios,
                            Ejercicio = bitacora.Ejercicio,
                            Eor = bitacora.Eor,
                            Folio = bitacora.Folio,
                            IdEmpleado = bitacora.IdEmpleado,
                            Estatus = bitacora.Estatus,
                            FechaCaptura = bitacora.FechaCaptura,
                           // Usuario = bitacora.IdEmpleado > 0 ? _mediator.Send(new GetUserByIdEmpleadoQuery(bitacora.IdEmpleado)).Result.Data.Login : "SIN USUARIO"



                        });
                    }

                }

                var responsables = await _oficioResponsableRepository.GetOficioReponsableByEjercicioFolioEor(oficio.Ejercicio, oficio.Folio, oficio.Eor);
                if (responsables != null)
                {
                    foreach (var responsable in responsables)
                    {
                        oficio.OficiosResponsables.Add(new OficioResponsableDto
                        {
                            Ejercicio = responsable.Ejercicio,
                            Eor = responsable.Eor,
                            Folio = responsable.Folio,
                            IdEmpleado = responsable.IdEmpleado,
                            Rol = responsable.Rol
                        });

                    }
                }
            }


            var mappedEntities = _mapper.Map<List<OficioDto>>(oficios);
            return Result<List<OficioDto>>.Success(mappedEntities);

        }


    }
}
