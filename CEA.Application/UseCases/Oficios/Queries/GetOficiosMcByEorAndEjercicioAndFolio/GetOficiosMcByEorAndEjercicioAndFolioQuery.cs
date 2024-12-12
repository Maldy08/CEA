using AutoMapper;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Domain.Entities.Oficios;
using CEA.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficiosMcByEorAndEjercicioAndFolio
{

    public record GetOficiosMcByEorAndEjercicioAndFolioQuery : IRequest<Result<Oficio>>
    {
        public int Eor { get; set; }
        public int Ejercicio { get; set; }
        public int Folio { get; set; }

        public GetOficiosMcByEorAndEjercicioAndFolioQuery(int ejercicio, int folio, int eor)
        {
            Eor = eor;
            Ejercicio = ejercicio;
            Folio = folio;
        }
    }
    internal class GetOficiosMcByEorAndEjercicioAndFolioQueryHandler : IRequestHandler<GetOficiosMcByEorAndEjercicioAndFolioQuery, Result<Oficio>>
    {

        private readonly IOficioRepository _oficioRepository;
        private readonly IOficioBitacoraRepository _oficioBitacoraRepository;
        private readonly IOficioResponsableRepository _oficioResponsableRepository;
        private readonly IMapper _mapper;

        public GetOficiosMcByEorAndEjercicioAndFolioQueryHandler(IOficioRepository oficioRepository, IOficioBitacoraRepository oficioBitacoraRepository, IOficioResponsableRepository oficioResponsableRepository, IMapper mapper)
        {
            _oficioRepository = oficioRepository;
            _oficioBitacoraRepository = oficioBitacoraRepository;
            _oficioResponsableRepository = oficioResponsableRepository;
            _mapper = mapper;
        }

        public async Task<Result<Oficio>> Handle(GetOficiosMcByEorAndEjercicioAndFolioQuery request, CancellationToken cancellationToken)
        {
            var oficios = await _oficioRepository.GetOficio(request.Ejercicio, request.Folio, request.Eor);
            if (oficios != null)
            
            {
                var bitacoras = await _oficioBitacoraRepository.GetOficioBitacoraByEjercicioFolioEor(oficios.Ejercicio, oficios.Folio, oficios.Eor);
                if (bitacoras != null)
                {
                    foreach (var bitacora in bitacoras)
                    {
                        oficios.OficioBitacora.Add(new OficioBitacora
                        {
                            Comentarios = bitacora.Comentarios,
                            Ejercicio = bitacora.Ejercicio
                           ,
                            Eor = bitacora.Eor,
                            Folio = bitacora.Folio,
                            IdEmpleado = bitacora.IdEmpleado,
                            Estatus = bitacora.Estatus,
                            FechaCaptura = bitacora.FechaCaptura,

                        });
                    }

                }

                var responsables = await _oficioResponsableRepository.GetOficioReponsableByEjercicioFolioEor(oficios.Ejercicio, oficios.Folio, oficios.Eor);
                if (responsables != null)
                {
                    foreach (var responsable in responsables)
                    {
                        oficios!.OficiosResponsables.Add(new OficioResponsable
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

           var result = _mapper.Map<Oficio>(oficios);
            return Result<Oficio>.Success(result);
        }
    }
}
