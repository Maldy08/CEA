namespace CEA.Domain.Entities.Nomina
{
    // Encabezado de un trámite de RH (NOM_TRAMITES). Por ahora solo vacaciones; viáticos tiene sus propias tablas.
    // TipoTramite → RH_CAT_TIPOS_TRAMITE, Estatus → RH_CAT_ESTATUS (con FK).
    public class NomTramite
    {
        public int IdTramite { get; set; }
        public int Ano { get; set; }
        // Folio oficial, <SIGLAS>-NNNN-AAAA (RH_CAT_TIPOS_TRAMITE.SIGLAS), p. ej. VAC-0015-2026. Único por año.
        public string Folio { get; set; } = string.Empty;
        public int TipoTramite { get; set; }
        // Beneficiario.
        public int IdEmpleado { get; set; }
        // 'P' autogestión, 'S' capturado por el supervisor (IdEmpleadoSupervisor).
        public string Ambito { get; set; } = "P";
        public int? IdEmpleadoSupervisor { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public int Estatus { get; set; }
        public DateTime? FechaAutorizacion { get; set; }
        public DateTime? FechaCancelacion { get; set; }
        // Quienes firman (FN_TRAMITES_CONSULTA_FIRMAS: FIRMA_1 / FIRMA_2), fijados al crear el trámite.
        public int? Valida1 { get; set; }
        public int? Valida2 { get; set; }
    }
}
