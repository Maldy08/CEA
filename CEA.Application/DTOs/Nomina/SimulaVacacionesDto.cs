namespace CEA.Application.DTOs.Nomina
{
    // Renglón de F_SIMULA_VACACIONES: días que se toman de cada periodo (año/semestre) y lo que le queda.
    public class SimulaVacacionesDto
    {
        public int NoEmp { get; set; }
        public int Ano { get; set; }
        // decimal: NOM_VACAPRIMA.DIAS / 2 puede dar medios días.
        public decimal Dias { get; set; }
        public int Periodo { get; set; }
        public decimal DiasXDisfrutar { get; set; }
    }
}
