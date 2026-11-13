using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetFirmasTramite
{
    public record GetFirmasTramiteQuery : IRequest<Result<FirmasTramiteDto>>
    {
        public int NoEmp { get; init; }
        public int Depto { get; init; }

        public GetFirmasTramiteQuery(int noEmp, int depto)
        {
            NoEmp = noEmp;
            Depto = depto;
        }
    }
    internal class GetFirmasTramiteQueryHandler : IRequestHandler<GetFirmasTramiteQuery, Result<FirmasTramiteDto>>
    {
        private readonly INomTramitesFunctions _nomTramitesFunctions;

        public GetFirmasTramiteQueryHandler(INomTramitesFunctions nomTramitesFunctions)
        {
            _nomTramitesFunctions = nomTramitesFunctions;
        }

        public async Task<Result<FirmasTramiteDto>> Handle(GetFirmasTramiteQuery request, CancellationToken cancellationToken)
        {
            var firmas = await _nomTramitesFunctions.ConsultaFirmas(request.NoEmp, request.Depto);
            if (firmas == null)
            {
                return await Result<FirmasTramiteDto>.FailureAsync($"No se encontraron firmas para el empleado {request.NoEmp} del departamento {request.Depto}.");
            }
            return await Result<FirmasTramiteDto>.SuccessAsync(firmas);
        }
    }
}
