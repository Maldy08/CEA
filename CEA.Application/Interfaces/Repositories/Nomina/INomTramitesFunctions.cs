using CEA.Application.DTOs.Nomina;

namespace CEA.Application.Interfaces.Repositories.Nomina
{
    public interface INomTramitesFunctions
    {
        Task<FirmasTramiteDto?> ConsultaFirmas(int noEmp, int depto);
        // Igual, con el depto de la ficha del empleado (EMPLEADOS.DEPTOCOMI).
        Task<FirmasTramiteDto?> ConsultaFirmas(int noEmp);
        // Formato impreso del trámite (VS_TRAMITES_FORMATO + _DET); null si no existe.
        Task<FormatoTramiteDto?> Formato(int idTramite);
        // El usuario (no. de empleado) es supervisor del empleado: TRAMITES_NIVEL >= 2 y el empleado tiene
        // SUPERVISOR_ID = él, o (suplencia) SUPERVISOR_ID no nulo y el mismo DEPTOCOMI que la ficha del usuario.
        // Misma regla que supervisaA del front (next-tramites-cea shared/server/empleados.ts).
        Task<bool> SupervisaA(int noEmpUsuario, int noEmp);
        // Administrador de RH del portal (USUARIOS.TRAMITES_NIVEL = 9): ve y opera todos los trámites de NOM_TRAMITES.
        // Misma regla que esAdministradorRh del front (next-tramites-cea shared/empleados.ts).
        Task<bool> EsAdministradorRh(int noEmp);
    }
}
