namespace CEA.Domain.Entities.Nomina
{
    // Solicitud de vacaciones de un trámite (NOM_VACAEMP_M). El desglose por periodo va en NOM_VACAEMP_D.
    // ORIGEN_REGISTRO 'T' = trámite del portal (con ID_TRAMITE y FOLIO); 'R' = saldo inicial/ajuste capturado por RH
    // (sin trámite). F_SALDO_VACACIONES toma el renglón con FHASTA más reciente del empleado, sea T o R.
    public class NomVacaempM
    {
        public const string OrigenTramite = "T";
        public const string OrigenRh = "R";

        public int IdVacaemp { get; set; }
        public int? IdTramite { get; set; }
        public int Ano { get; set; }
        public string? Folio { get; set; }
        public int NoEmp { get; set; }
        // Días hábiles del rango FDesde-FHasta.
        public int DiasSolicitados { get; set; }
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        // Primer día de regreso al trabajo.
        public DateTime? FechaRegreso { get; set; }
        // Relevo opcional.
        public int? IdEmpleadoCubre { get; set; }
        public string OrigenRegistro { get; set; } = OrigenTramite;
        public int? IdEmpleadoCaptura { get; set; }
        public DateTime? FechaCaptura { get; set; }
    }
}
