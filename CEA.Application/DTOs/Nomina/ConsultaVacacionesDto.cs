namespace CEA.Application.DTOs.Nomina
{
    // FN_CONSULTA_VACACIONES ya interpretada: días hábiles del rango (sin fines de semana ni festivos del empleado),
    // festivos que no se contaron y el reparto de esos días entre los periodos (F_SIMULA_VACACIONES).
    public class ConsultaVacacionesDto
    {
        public int DiasHabiles { get; set; }
        // "24/12/2026 (Noche Buena), 25/12/2026 (Navidad)"; nulo si no cayó ninguno.
        public string? Festivos { get; set; }
        // Vacío cuando no hay días hábiles.
        public List<SimulaVacacionesDto> Periodos { get; set; } = new();
    }
}
