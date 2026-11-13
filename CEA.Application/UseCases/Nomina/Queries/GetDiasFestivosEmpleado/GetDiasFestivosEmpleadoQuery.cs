using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetDiasFestivosEmpleado
{
    public record GetDiasFestivosEmpleadoQuery : IRequest<Result<List<NomDiasFestivosDto>>>
    {
        public int NoEmp { get; init; }
        public DateTime Desde { get; init; }
        public DateTime Hasta { get; init; }

        public GetDiasFestivosEmpleadoQuery(int noEmp, DateTime desde, DateTime hasta)
        {
            NoEmp = noEmp;
            Desde = desde;
            Hasta = hasta;
        }
    }
    internal class GetDiasFestivosEmpleadoQueryHandler : IRequestHandler<GetDiasFestivosEmpleadoQuery, Result<List<NomDiasFestivosDto>>>
    {
        private readonly INomDiasFestivosRepository _nomDiasFestivosRepository;

        public GetDiasFestivosEmpleadoQueryHandler(INomDiasFestivosRepository nomDiasFestivosRepository)
        {
            _nomDiasFestivosRepository = nomDiasFestivosRepository;
        }

        public async Task<Result<List<NomDiasFestivosDto>>> Handle(GetDiasFestivosEmpleadoQuery request, CancellationToken cancellationToken)
        {
            if (request.Hasta < request.Desde)
            {
                return await Result<List<NomDiasFestivosDto>>.FailureAsync("La fecha final no puede ser anterior a la inicial.");
            }
            var festivos = await _nomDiasFestivosRepository.GetByEmpleadoAsync(request.NoEmp, request.Desde, request.Hasta);
            return await Result<List<NomDiasFestivosDto>>.SuccessAsync(festivos);
        }
    }
}
