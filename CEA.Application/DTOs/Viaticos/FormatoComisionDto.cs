using CEA.Application.Common.Mappings;

namespace CEA.Application.DTOs.Viaticos
{
    public class FormatoComisionDto : IMapFrom<FormatoComisionDto>
    {
        public int Oficina { get; set; }
        public int Ejercicio { get; set; }
        public int NoViat { get; set; }
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int NoEmp { get; set; }
        public int OrigenId { get; set; }
        public int DestinoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public DateTime FechaSal { get; set; } = DateTime.Now;
        public DateTime FechaReg { get; set; } = DateTime.Now;
        public int Dias { get; set; }
        public string InforAct { get; set; } = string.Empty;
        public string InforResul { get; set; } = string.Empty;
        public double Importe { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Materno { get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty;
        public string DescripcionPuesto { get; set; } = string.Empty;
        public string CdOrigen { get; set; } = string.Empty;
        public string CdDestino { get; set; } = string.Empty;
        public string QuienLoComisiona { get; set; } = string.Empty;
        public string PuestoQuienLoComisiona { get; set; } = string.Empty;
        public string EdoOrigen { get; set; } = string.Empty;
        public string EdoDestino { get; set; } = string.Empty;
        public string DeptoDescripcion { get; set; } = string.Empty;
        public string DirectorAdministrativo { get; set; } = string.Empty;
    }
}
