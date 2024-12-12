

using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.GetAllEmpleados
{
    public record GetAllEmpleadosQuery : IRequest<Result<IEnumerable<EmpleadoDto>>>
    {
        public GetAllEmpleadosQuery()
        {

        }
    }
    internal class GetAllEmpleadosQueryHanlder : IRequestHandler<GetAllEmpleadosQuery, Result<IEnumerable<EmpleadoDto>>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetAllEmpleadosQueryHanlder(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmpleadoDto>>> Handle(GetAllEmpleadosQuery request, CancellationToken cancellationToken)
        {
            var empleados = await _empleadoRepository.GetEmpleadosAsync();
            var empleadodto = _mapper.Map<IEnumerable<EmpleadoDto>>(empleados);
            return Result<IEnumerable<EmpleadoDto>>.Success(empleadodto);
        }
    }
}
