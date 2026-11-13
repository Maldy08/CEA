namespace CEA.Domain.Entities.Nomina
{
    // Solicitud de vacaciones de un trámite (NOM_VACAEMP_M). El desglose por periodo va en NOM_VACAEMP_D.
    public class NomVacaempM
    {
        public int IdVacaemp { get; set; }
        public int IdTramite { get; set; }
        public int Ano { get; set; }
        public string Folio { get; set; } = string.Empty;
        public int NoEmp { get; set; }
        // Días hábiles del rango FDesde-FHasta.
        public int DiasSolicitados { get; set; }
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        // Primer día de regreso al trabajo.
        public DateTime? FechaRegreso { get; set; }
        // Relevo opcional.
        public int? IdEmpleadoCubre { get; set; }
    }
}
