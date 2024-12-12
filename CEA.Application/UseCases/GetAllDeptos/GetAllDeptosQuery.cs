using AutoMapper;
using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Shared.Interfaces;
using MediatR;


namespace CEA.Application.Features.GetAllDeptos
{
    public record GetAllDeptosQuery : IRequest<Result<IEnumerable<DeptoUeDto>>>
    {
        public GetAllDeptosQuery()
        {

        }
    }
    internal class GetAllDeptosQueryHandler : IRequestHandler<GetAllDeptosQuery, Result<IEnumerable<DeptoUeDto>>>
    {

        private readonly IDeptoRepository _deptoRepository;
        private readonly IMapper _mapper;

        public GetAllDeptosQueryHandler(IDeptoRepository deptoRepository, IMapper mapper)
        {
            _deptoRepository = deptoRepository;
            _mapper = mapper;
        }
        public Task<Result<IEnumerable<DeptoUeDto>>> Handle(GetAllDeptosQuery request, CancellationToken cancellationToken)
        {
            var deptos = _deptoRepository.GetDeptosAsync();
            var deptodto = _mapper.Map<IEnumerable<DeptoUeDto>>(deptos);
            return Result<IEnumerable<DeptoUeDto>>.SuccessAsync(deptodto);
        }
    }
    
    
}
