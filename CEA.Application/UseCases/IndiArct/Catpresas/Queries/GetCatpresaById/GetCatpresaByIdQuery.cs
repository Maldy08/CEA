using AutoMapper;
using CEA.Application.DTOs.IndiArct;
using CEA.Application.Interfaces.Repositories.IndiArct;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.IndiArct.Catpresas.Queries.GetCatpresaById
{
    public record GetCatpresaByIdQuery(int Id) : IRequest<Result<IndiArctCatpresasDto>>;

    internal class GetCatpresaByIdQueryHandler : IRequestHandler<GetCatpresaByIdQuery, Result<IndiArctCatpresasDto>>
    {
        private readonly IIndiArctCatpresasRepository _repository;
        private readonly IMapper _mapper;

        public GetCatpresaByIdQueryHandler(IIndiArctCatpresasRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IndiArctCatpresasDto>> Handle(GetCatpresaByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                return Result<IndiArctCatpresasDto>.Failure("Presa no encontrada");
            var dto = _mapper.Map<IndiArctCatpresasDto>(entity);
            return Result<IndiArctCatpresasDto>.Success(dto);
        }
    }
}
