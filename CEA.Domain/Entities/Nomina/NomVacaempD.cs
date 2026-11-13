namespace CEA.Domain.Entities.Nomina
{
    // Días de una solicitud que se toman de un periodo vacacional (NOM_VACAEMP_D, mismas columnas que NOM_VACAEMP).
    public class NomVacaempD
    {
        public int IdVacaemp { get; set; }
        public int NoEmp { get; set; }
        public int Ano { get; set; }
        // Movimiento en NOM_VACAEMP; nulo mientras no se aplique.
        public int? NoMov { get; set; }
        public DateTime? FDesde { get; set; }
        public DateTime? FHasta { get; set; }
        public int DiasVac { get; set; }
        // 'AAAA-N', p. ej. '2025-2'.
        public string PerVac { get; set; } = string.Empty;
        // Lo que le queda al periodo después de la solicitud.
        public int DiasXDisf { get; set; }
    }
}
