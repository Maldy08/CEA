namespace CEA.Application.DTOs.Nomina
{
    // Trámite de RH (NOM_TRAMITES). Bitacora solo viene llena en el detalle (GetTramiteById); en listados va vacía.
    public class NomTramiteDto
    {
        public int IdTramite { get; set; }
        public int Ano { get; set; }
        public string Folio { get; set; } = string.Empty;
        public int TipoTramite { get; set; }
        public int IdEmpleado { get; set; }
        public string Ambito { get; set; } = "P";
        public int? IdEmpleadoSupervisor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int Estatus { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        public int? Valida1 { get; set; }
        public int? Valida2 { get; set; }
        public List<NomTramiteBitacoraDto> Bitacora { get; set; } = new();
    }

    public class NomTramiteBitacoraDto
    {
        public int IdBitacora { get; set; }
        public DateTime FechaMovimiento { get; set; }
        public int IdEmpleado { get; set; }
        public int IdEstatus { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string? Comentarios { get; set; }
    }
}
