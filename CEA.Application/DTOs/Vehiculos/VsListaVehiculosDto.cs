using CEA.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEA.Application.DTOs.Vehiculos
{
    public class VsListaVehiculosDto: IMapFrom<VsListaVehiculosDto>
    {
        public int Numero { get; set; }
        public int Ano { get; set; }
        public string NoActivo { get; set; } = string.Empty;
        public string Placas { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
        public double Odometro { get; set; } = 0;
        public int Estatus { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime? FUltServ { get; set; } = DateTime.Now;  
        public DateTime? FProxServ { get; set; } = DateTime.Now;
        public string Tipo { get; set; } = string.Empty;
        public string? Capacidad { get; set; } = string.Empty;
        public int Pernoc { get; set; }
        public string Comentarios { get; set; } = string.Empty;
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public string Serie { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty; 
        public DateTime FechaAdq { get; set; } = DateTime.Now;
        public int BmEstatus { get; set; }
        public double Importe { get; set; }
        public int Resguardo { get; set; }
        public string Depto { get; set; } = string.Empty;
        public string? Resguardante { get; set; } = string.Empty;
        public string? NombreAseg{ get; set; } = string.Empty;
        public string? NoSeguro { get; set; } = string.Empty;
        public DateTime? Vigencia { get; set; } =DateTime.Now;
    }
}
