
namespace CEA.Domain.Entities.Vehiculos
{
    public class VhCatVehiculos
    {
        public int NoEcon { get; set; }
        public string NoActivo { get; set; } = string.Empty;
        public int Ano { get; set; }
        public int Placas { get; set; }
        public string Color { get; set; } = string.Empty;   
        public double Odometro { get; set; }
        public int Estatus { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FUltServ { get; set; }
        public DateTime FProxServ{ get; set; }
        public int Tipo { get; set; }
        public string Capacidad { get; set; } = string.Empty;
        public int Pernoc { get; set; }
        public string Comentarios { get; set; } = string.Empty;
    }
}
