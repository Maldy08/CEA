using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.Nomina;

namespace CEA.Application.DTOs.Nomina
{
    public class NomDiasFestivosDto : IMapFrom<NomDiasFestivos>
    {
        public int IdFestivo { get; set; }
        public DateTime FechaFestivo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public DateTime? FechaOficial { get; set; }
        // Ambas nulas = día completo; con alguna = horario especial ('HH:MI').
        public string? HoraEntrada { get; set; }
        public string? HoraSalida { get; set; }
    }
}
