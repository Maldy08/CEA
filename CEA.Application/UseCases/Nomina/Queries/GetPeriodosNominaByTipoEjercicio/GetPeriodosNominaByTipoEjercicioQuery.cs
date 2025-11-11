using AutoMapper;
using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetPeriodosNominaByTipoEjercicio
{

    public record GetPeriodosNominaByTipoEjercicio : IRequest<Result<List<NomPeriodosDto>>>
    {
        public int TipoNomina { get; init; }
        public int AnoEjercicio { get; init; }


        public GetPeriodosNominaByTipoEjercicio(int tipoNomina, int anoEjercicio)
        {
            TipoNomina = tipoNomina;
            AnoEjercicio = anoEjercicio;
        }

    }
    internal class GetPeriodosNominaByTipoEjercicioQueryHandler : IRequestHandler<GetPeriodosNominaByTipoEjercicio, Result<List<NomPeriodosDto>>>
    {

        private readonly IMapper _mapper;
        private readonly INomPeriodosRepository _nomPeriodosRepository;

        public GetPeriodosNominaByTipoEjercicioQueryHandler(INomPeriodosRepository nomPeriodosRepository, IMapper mapper)
        {
            _nomPeriodosRepository = nomPeriodosRepository;
            _mapper = mapper;
        }


        public async Task<Result<List<NomPeriodosDto>>> Handle(GetPeriodosNominaByTipoEjercicio request, CancellationToken cancellationToken)
        {
            var periodos = await _nomPeriodosRepository.GetNomPeriodosByTipoNomEjercicioAsync(request.TipoNomina, request.AnoEjercicio);
            var mappedPeriodos = _mapper.Map<List<NomPeriodosDto>>(periodos);
            return await Result<List<NomPeriodosDto>>.SuccessAsync(mappedPeriodos);

        }
    }
}
