using AutoMapper;
using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Energia.Queries.GetEnergiaById
{
    public record GetEnergiaByIdQuery(int Id) : IRequest<Result<IndiArctEnergiaDto>>;

    internal class GetEnergiaByIdQueryHandler : IRequestHandler<GetEnergiaByIdQuery, Result<IndiArctEnergiaDto>>
    {
        private readonly IIndiArctEnergiaRepository _repository;
        private readonly IMapper _mapper;

        public GetEnergiaByIdQueryHandler(IIndiArctEnergiaRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IndiArctEnergiaDto>> Handle(GetEnergiaByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return Result<IndiArctEnergiaDto>.Failure("Registro de energía no encontrado");
            var dto = _mapper.Map<IndiArctEnergiaDto>(entity);
            return Result<IndiArctEnergiaDto>.Success(dto);
        }
    }
}
