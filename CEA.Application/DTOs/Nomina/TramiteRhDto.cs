namespace CEA.Application.DTOs.Nomina
{
    // Renglón de la Administración de Trámites de RH: el trámite con el nombre del empleado y un resumen de fechas.
    public class TramiteRhDto : NomTramiteDto
    {
        public string? NombreEmpleado { get; set; }
        public string? DescripcionDepto { get; set; }
        // Vacaciones: FDesde/FHasta de NOM_VACAEMP_M. Permisos y justificantes: FECHA_INICIO/FIN de NOM_PERMISOS_JUSTIFICANTES.
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        // Solo permisos y justificantes.
        public int? TipoIncidencia { get; set; }
    }

    // Detalle editable de un permiso o justificante (NOM_PERMISOS_JUSTIFICANTES).
    public class PermisoJustificanteDto
    {
        public int IdPermisoJustificante { get; set; }
        public int IdTramite { get; set; }
        public int TipoIncidencia { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string JustificacionMotivo { get; set; } = string.Empty;
        public int? IdEmpleadoCubre { get; set; }
    }

    // Lo que RH necesita para modificar un trámite: encabezado con bitácora y el detalle según su tipo.
    public class DetalleTramiteRhDto
    {
        public NomTramiteDto Tramite { get; set; } = new();
        public string? NombreEmpleado { get; set; }
        public SolicitudVacacionesDto? Vacaciones { get; set; }
        public PermisoJustificanteDto? PermisoJustificante { get; set; }
    }
}
