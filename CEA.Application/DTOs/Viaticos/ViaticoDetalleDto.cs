

using CEA.Application.Common.Mappings;

namespace CEA.Application.DTOs.Viaticos
{
    public class ViaticoDetalleDto : IMapFrom<ViaticoDetalleDto>
    {
        public string NoViatico { get; set; } = string.Empty;
        public DateTime Fecha { get; set; } = DateTime.Now;
        public int NoEmp { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Puesto { get; set; } = string.Empty;
        public string Depto { get; set; } = string.Empty;
        public int Origen { get; set; }
        public string OrigenNom { get; set; } = string.Empty;
        public int Destino { get; set; }
        public string DestinoNom { get; set; } = string.Empty;
        public string ComisionTitulo { get; set; } = string.Empty;
        public string ComisionDetalle { get; set; } = string.Empty;
        public DateTime FechaSalida { get; set; } = DateTime.Now;
        public DateTime FechaRegreso { get; set; } = DateTime.Now;
        public int Dias { get; set; }
        public int Estatus { get; set; }
        public double Importe { get; set; }
    }
}
