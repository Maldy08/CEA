using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetSimulaVacaciones
{
    public record GetSimulaVacacionesQuery : IRequest<Result<List<SimulaVacacionesDto>>>
    {
        public int NoEmp { get; init; }
        public int Dias { get; init; }

        public GetSimulaVacacionesQuery(int noEmp, int dias)
        {
            NoEmp = noEmp;
            Dias = dias;
        }
    }
    internal class GetSimulaVacacionesQueryHandler : IRequestHandler<GetSimulaVacacionesQuery, Result<List<SimulaVacacionesDto>>>
    {
        private readonly INomVacacionesFunctions _nomVacacionesFunctions;

        public GetSimulaVacacionesQueryHandler(INomVacacionesFunctions nomVacacionesFunctions)
        {
            _nomVacacionesFunctions = nomVacacionesFunctions;
        }

        public async Task<Result<List<SimulaVacacionesDto>>> Handle(GetSimulaVacacionesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _nomVacacionesFunctions.SimulaVacaciones(request.NoEmp, request.Dias);
            return await Result<List<SimulaVacacionesDto>>.SuccessAsync(entities);
        }
    }
}
