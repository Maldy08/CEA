namespace CEA.Domain.Entities.Nomina
{
    // Detalle de un trámite de permiso (TIPO_TRAMITE 3) o justificante (4): NOM_PERMISOS_JUSTIFICANTES, uno por trámite.
    // TipoIncidencia 1 PERMISO CON GOCE, 2 PERMISO SIN GOCE (→ trámite 3); 3 JUSTIF. ENTRADA, 4 JUSTIF. SALIDA,
    // 5 NO CHECADA ENTRADA, 6 NO CHECADA SALIDA, 7 NO CHECADA (→ trámite 4).
    public class NomPermisoJustificante
    {
        public int IdPermisoJustificante { get; set; }
        public int IdTramite { get; set; }
        public int TipoIncidencia { get; set; }
        // Puede llevar hora (justificantes). Un solo día: FechaFin = FechaInicio.
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string JustificacionMotivo { get; set; } = string.Empty;
        // Relevo opcional.
        public int? IdEmpleadoCubre { get; set; }
    }
}
