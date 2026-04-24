using AutoMapper;
using CEA.Application.DTOs.Bitacora;
using CEA.Application.Interfaces.Repositories.Bitacora;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Bitacora.Temas.Queries.GetTemaById
{
    public record GetTemaByIdQuery(int Id) : IRequest<Result<TemaDto>>;

    internal class GetTemaByIdQueryHandler : IRequestHandler<GetTemaByIdQuery, Result<TemaDto>>
    {
        private readonly ITemaRepository _temaRepository;
        private readonly IMapper _mapper;

        public GetTemaByIdQueryHandler(ITemaRepository temaRepository, IMapper mapper)
        {
            _temaRepository = temaRepository;
            _mapper = mapper;
        }

        public async Task<Result<TemaDto>> Handle(GetTemaByIdQuery request, CancellationToken cancellationToken)
        {
            var tema = await _temaRepository.GetByIdAsync(request.Id);
            if (tema == null)
                return Result<TemaDto>.Failure("Tema no encontrado");
            var temaDto = _mapper.Map<TemaDto>(tema);
            return Result<TemaDto>.Success(temaDto);
        }
    }
}
