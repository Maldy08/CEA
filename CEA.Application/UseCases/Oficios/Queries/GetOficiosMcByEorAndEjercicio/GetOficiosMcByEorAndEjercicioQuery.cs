

using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicio
{

    public record GetOficiosMcByEorAndEjercicioQuery : IRequest<Result<List<Oficio>>>
    {
        public int Eor { get; set; }
        public int Ejercicio { get; set; }

        public GetOficiosMcByEorAndEjercicioQuery(int eor, int ejercicio)
        {
            Eor = eor;
            Ejercicio = ejercicio;
        }
    }
    internal class GetOficiosMcByEorAndEjercicioQueryHandler : IRequestHandler<GetOficiosMcByEorAndEjercicioQuery, Result<List<Oficio>>>
    {
        private readonly IOficioRepository _oficioRepository;
        private readonly IOficioBitacoraRepository _oficioBitacoraRepository;
        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IMapper _mapper;

        public GetOficiosMcByEorAndEjercicioQueryHandler(IOficioRepository oficioRepository, IOficioBitacoraRepository oficioBitacoraRepository, IOficioResponsableRepository oficioResponsableRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _oficioBitacoraRepository = oficioBitacoraRepository;
            _oficioResponsableRepository = oficioResponsableRepository;
            _mapper = mapper;
        }
        public async Task<Result<List<Oficio>>> Handle(GetOficiosMcByEorAndEjercicioQuery request, CancellationToken cancellationToken)
        {

            var oficios = await _oficioRepository.GetOficiosMCByEjercicio(request.Eor, request.Ejercicio);
            foreach (var oficio in oficios)
            {
                var bitacoras = await _oficioBitacoraRepository.GetOficioBitacoraByEjercicioFolioEor(oficio.Ejercicio, oficio.Folio, oficio.Eor);
                if (bitacoras != null)
                {
                    foreach (var bitacora in bitacoras)
                    {
                        oficio.OficioBitacora.Add(new OficioBitacora { Comentarios = bitacora.Comentarios, Ejercicio = bitacora.Ejercicio
                           ,
                            Eor = bitacora.Eor,
                            Folio = bitacora.Folio,
                            IdEmpleado = bitacora.IdEmpleado,
                            Estatus = bitacora.Estatus,
                            FechaCaptura = bitacora.FechaCaptura,
                            
                        });
                    }

                    }

                var responsables = await _oficioResponsableRepository.GetOficioReponsableByEjercicioFolioEor(oficio.Ejercicio, oficio.Folio, oficio.Eor);
                if (responsables != null)
                {
                    foreach (var responsable in responsables)
                    {
                        oficio.OficiosResponsables.Add(new OficioResponsable
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


                var mappedEntities = _mapper.Map<List<Oficio>>(oficios);
                return Result<List<Oficio>>.Success(mappedEntities);
         
        }
    
    
    }
}
