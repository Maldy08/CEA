namespace CEA.Application.DTOs.Nomina
{
    // Solicitud de vacaciones de un trámite (NOM_VACAEMP_M) con su reparto por periodo (NOM_VACAEMP_D).
    public class SolicitudVacacionesDto
    {
        public int IdVacaemp { get; set; }
        public int IdTramite { get; set; }
        public int Ano { get; set; }
        public string Folio { get; set; } = string.Empty;
        public int NoEmp { get; set; }
        public int DiasSolicitados { get; set; }
        public DateTime FDesde { get; set; }
        public DateTime FHasta { get; set; }
        public DateTime? FechaRegreso { get; set; }
        public int? IdEmpleadoCubre { get; set; }
        public List<SolicitudVacacionesPeriodoDto> Periodos { get; set; } = new();
    }

    public class SolicitudVacacionesPeriodoDto
    {
        // 'AAAA-N', p. ej. '2025-2'.
        public string PerVac { get; set; } = string.Empty;
        public int Ano { get; set; }
        public int DiasVac { get; set; }
        public int DiasXDisf { get; set; }
        // Movimiento en NOM_VACAEMP; nulo mientras no se aplique.
        public int? NoMov { get; set; }
        public DateTime? FDesde { get; set; }
        public DateTime? FHasta { get; set; }
    }
}
