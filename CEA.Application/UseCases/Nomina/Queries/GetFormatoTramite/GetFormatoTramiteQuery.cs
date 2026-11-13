using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Shared.Interfaces;
using MediatR;

namespace CEA.Application.UseCases.Nomina.Queries.GetFormatoTramite
{
    // Datos del formato oficial impreso de cualquier trámite de RH (VS_TRAMITES_FORMATO + _DET).
    // NoEmpUsuario = quien consulta (claim "userid" del token). Lo puede ver:
    //   1) el empleado del trámite;
    //   2) quien lo firma: jefe (VALIDA_1), director (VALIDA_2) o responsable de RH (DEPTOS_UE 24 EMP_RESPON);
    //   3) su supervisor (SupervisaA);
    //   4) el administrador de RH del portal (TRAMITES_NIVEL = 9).
    // Si no existe o no tiene permiso responde el MISMO mensaje: no revela qué trámites existen.
    public record GetFormatoTramiteQuery(int IdTramite, int NoEmpUsuario) : IRequest<Result<FormatoTramiteDto>>;

    internal class GetFormatoTramiteQueryHandler : IRequestHandler<GetFormatoTramiteQuery, Result<FormatoTramiteDto>>
    {
        private readonly INomTramitesFunctions _nomTramitesFunctions;

        public GetFormatoTramiteQueryHandler(INomTramitesFunctions nomTramitesFunctions)
        {
            _nomTramitesFunctions = nomTramitesFunctions;
        }

        public async Task<Result<FormatoTramiteDto>> Handle(GetFormatoTramiteQuery request, CancellationToken cancellationToken)
        {
            var formato = await _nomTramitesFunctions.Formato(request.IdTramite);
            if (formato == null || !await PuedeVer(request.NoEmpUsuario, formato.Encabezado))
            {
                return await Result<FormatoTramiteDto>.FailureAsync(
                    $"El trámite {request.IdTramite} no existe o no tiene permiso para consultarlo.");
            }
            return await Result<FormatoTramiteDto>.SuccessAsync(formato);
        }

        private async Task<bool> PuedeVer(int usuario, FormatoTramiteEncabezadoDto e)
        {
            if (usuario == e.NoEmp) return true;
            if (usuario == e.Firma1NoEmp || usuario == e.Firma2NoEmp || usuario == e.Firma3NoEmp) return true;
            if (await _nomTramitesFunctions.SupervisaA(usuario, e.NoEmp)) return true;
            return await _nomTramitesFunctions.EsAdministradorRh(usuario);
        }
    }
}
