using AutoMapper;
using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.Features.Viaticos.Queries.GetViaticoCiudadesByEstado
{

    public record GetViaticoCiudadesByEstadoQuery : IRequest<Result<List<ViaticoCiudadDto>>>
    {
        public int IdEstado { get; set; }

        public GetViaticoCiudadesByEstadoQuery(int idEstado)
        {
            IdEstado = idEstado;
        }
   }
    internal class GetViaticoCiudadesByEstadoQueryHandler : IRequestHandler<GetViaticoCiudadesByEstadoQuery, Result<List<ViaticoCiudadDto>>>
    {

        private readonly IMapper _mapper;
        private readonly IViaticoCiudadRepository _viaticoCiudadRepository;

        public GetViaticoCiudadesByEstadoQueryHandler(IMapper mapper, IViaticoCiudadRepository viaticoCiudadRepository)
        {
            _mapper = mapper;
            _viaticoCiudadRepository = viaticoCiudadRepository;
        }
        public async  Task<Result<List<ViaticoCiudadDto>>> Handle(GetViaticoCiudadesByEstadoQuery request, CancellationToken cancellationToken)
        {
           var entities = await _viaticoCiudadRepository.GetByIdEstado(request.IdEstado);
            var viaticos = _mapper.Map<List<ViaticoCiudadDto>>(entities);
            return await Result<List<ViaticoCiudadDto>>.SuccessAsync(viaticos);
        }
    }
}
