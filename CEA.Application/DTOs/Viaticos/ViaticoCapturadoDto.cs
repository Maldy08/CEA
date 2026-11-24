namespace CEA.Application.DTOs.Viaticos
{
    // Viático que el usuario capturó para otro empleado (NOEMP_CREA = él). Bandeja → "Capturados por mí".
    public class ViaticoCapturadoDto
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }
        public int NoEmp { get; set; }
        public string? NombreEmpleado { get; set; }
    }
}
