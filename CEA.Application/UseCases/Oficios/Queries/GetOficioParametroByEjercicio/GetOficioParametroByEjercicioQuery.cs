using AutoMapper;
using CEA.Application.DTOs.Oficios;
using CEA.Application.Interfaces.Repositories.Oficios;
using CEA.Shared.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.UseCases.Oficios.Queries.GetOficioParametroByEjercicio
{
    public record GetOficioParametroByEjercicioQuery : IRequest<Result<OficioParametroDto>>
    {
        public int Ejercicio { get; set; }

        public GetOficioParametroByEjercicioQuery(int ejercicio)
        {
            Ejercicio = ejercicio;
        }
    }
    internal class GetOficioParametroByEjercicioQueryHandler : IRequestHandler<GetOficioParametroByEjercicioQuery, Result<OficioParametroDto>>
    {

        private readonly IOficioParametroRepository _oficioParametroRepository;
        private readonly IMapper _mapper;

        public GetOficioParametroByEjercicioQueryHandler(IOficioParametroRepository oficioParametroRepository, IMapper mapper)
        {
            _oficioParametroRepository = oficioParametroRepository;
            _mapper = mapper;
        }
        public async Task<Result<OficioParametroDto>> Handle(GetOficioParametroByEjercicioQuery request, CancellationToken cancellationToken)
        {
            var oficioParametro = await _oficioParametroRepository.GetOficioParametroByEjercicio(request.Ejercicio);
            var oficioParametroDto = _mapper.Map<OficioParametroDto>(oficioParametro);
            return Result<OficioParametroDto>.Success(oficioParametroDto);
        }
    }
}
