using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Viaticos.Queries.ViaticosCapturados
{
    // Bandeja del supervisor (TRAMITES_NIVEL 2) → "Capturados por mí": viáticos que capturó para otros empleados.
    public record ViaticosCapturadosQuery(int Ejercicio, int NoEmpUsuario) : IRequest<Result<List<ViaticoCapturadoDto>>>;

    internal class ViaticosCapturadosQueryHandler : IRequestHandler<ViaticosCapturadosQuery, Result<List<ViaticoCapturadoDto>>>
    {
        private readonly IViaticoPorEmpleadoDto _viaticos;
        private readonly INomTramitesFunctions _nomTramitesFunctions;

        public ViaticosCapturadosQueryHandler(IViaticoPorEmpleadoDto viaticos, INomTramitesFunctions nomTramitesFunctions)
        {
            _viaticos = viaticos;
            _nomTramitesFunctions = nomTramitesFunctions;
        }

        public async Task<Result<List<ViaticoCapturadoDto>>> Handle(ViaticosCapturadosQuery request, CancellationToken cancellationToken)
        {
            if (!await _nomTramitesFunctions.EsSupervisor(request.NoEmpUsuario))
                return await Result<List<ViaticoCapturadoDto>>.FailureAsync("No tiene personal a su cargo.");
            return await Result<List<ViaticoCapturadoDto>>.SuccessAsync(await _viaticos.GetCapturadosAsync(request.Ejercicio, request.NoEmpUsuario));
        }
    }
}
