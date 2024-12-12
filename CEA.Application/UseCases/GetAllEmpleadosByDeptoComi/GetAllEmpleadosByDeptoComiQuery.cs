

using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.GetAllEmpleadosByDeptoComi
{
    public record GetAllEmpleadosByDeptoComiQuery : IRequest<Result<IEnumerable<EmpleadoDto>>>
    {
        public int Id { get; set; }
        public GetAllEmpleadosByDeptoComiQuery(int id)
        {
            Id = id;
        }
    }

    internal class GetAllEmpleadosByDeptoComiQueryHandler : IRequestHandler<GetAllEmpleadosByDeptoComiQuery, Result<IEnumerable<EmpleadoDto>>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetAllEmpleadosByDeptoComiQueryHandler(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmpleadoDto>>> Handle(GetAllEmpleadosByDeptoComiQuery request, CancellationToken cancellationToken)
        {
            var empleados = await _empleadoRepository.GetEmpleadosByDeptoComi(request.Id);
            var empleadodto = _mapper.Map<IEnumerable<EmpleadoDto>>(empleados);
            return Result<IEnumerable<EmpleadoDto>>.Success(empleadodto);

        }
    }
}
