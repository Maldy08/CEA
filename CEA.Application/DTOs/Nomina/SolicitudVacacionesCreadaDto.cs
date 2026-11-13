namespace CEA.Application.DTOs.Nomina
{
    // Resultado de guardar una solicitud de vacaciones (NOM_TRAMITES + NOM_VACAEMP_M).
    public class SolicitudVacacionesCreadaDto
    {
        public int IdTramite { get; set; }
        public int IdVacaemp { get; set; }
        // <SIGLAS>-NNNN-AAAA, p. ej. VAC-0015-2026.
        public string Folio { get; set; } = string.Empty;
        public DateTime FechaRegreso { get; set; }
    }
}
