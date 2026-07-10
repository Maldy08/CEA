using AutoMapper;
using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.PresasNiveles.Queries.GetPresaNivelById
{
    public record GetPresaNivelByIdQuery(int Id) : IRequest<Result<IndiArctPresasNivelesDto>>;

    internal class GetPresaNivelByIdQueryHandler : IRequestHandler<GetPresaNivelByIdQuery, Result<IndiArctPresasNivelesDto>>
    {
        private readonly IIndiArctPresasNivelesRepository _repository;
        private readonly IMapper _mapper;

        public GetPresaNivelByIdQueryHandler(IIndiArctPresasNivelesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IndiArctPresasNivelesDto>> Handle(GetPresaNivelByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return Result<IndiArctPresasNivelesDto>.Failure("Captura de volumen no encontrada");
            var dto = _mapper.Map<IndiArctPresasNivelesDto>(entity);
            return Result<IndiArctPresasNivelesDto>.Success(dto);
        }
    }
}
