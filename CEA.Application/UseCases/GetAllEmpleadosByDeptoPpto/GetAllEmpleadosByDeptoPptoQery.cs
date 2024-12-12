

using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.GetAllEmpleadosByDeptoPpto
{
    public record class GetAllEmpleadosByDeptoPptoQery : IRequest<Result<IEnumerable<EmpleadoDto>>>
    {
        public int Id { get; set; }
        public GetAllEmpleadosByDeptoPptoQery(int id)
        {
            Id = id;
        }
    }
    internal class GetAllEmpleadosByDeptoPptoQeryHandler : IRequestHandler<GetAllEmpleadosByDeptoPptoQery, Result<IEnumerable<EmpleadoDto>>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetAllEmpleadosByDeptoPptoQeryHandler(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<EmpleadoDto>>> Handle(GetAllEmpleadosByDeptoPptoQery request, CancellationToken cancellationToken)
        {
            var empleados = await _empleadoRepository.GetEmpleadosByDeptoPpto(request.Id);
            var empleadodto = _mapper.Map<IEnumerable<EmpleadoDto>>(empleados);
            return Result<IEnumerable<EmpleadoDto>>.Success(empleadodto);
        }
    }
}
