

using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.Features.GetEmpleadoById
{
    public record  GetEmpleadoByIdQuery : IRequest<Result<EmpleadoDto>>
    {
        public int Id { get; set; }
        public GetEmpleadoByIdQuery(int id)
        {
            Id = id;
        }
    }
    internal class GetEmpleadoByIdQueryHandler : IRequestHandler<GetEmpleadoByIdQuery, Result<EmpleadoDto>>
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        private readonly IMapper _mapper;

        public GetEmpleadoByIdQueryHandler(IEmpleadoRepository empleadoRepository, IMapper mapper)
        {
            _empleadoRepository = empleadoRepository;
            _mapper = mapper;
        }
        public async Task<Result<EmpleadoDto>> Handle(GetEmpleadoByIdQuery request, CancellationToken cancellationToken)
        {
            var empleado = await _empleadoRepository.GetEmpleadoByIdAsync(request.Id);
            var empleadodto = _mapper.Map<EmpleadoDto>(empleado);
            return Result<EmpleadoDto>.Success(empleadodto);
        }
    }
}
