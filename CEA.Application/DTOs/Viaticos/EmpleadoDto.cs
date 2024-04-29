using CEA.Application.Common.Mappings;
using CEA.Domain.Entities.RecursosHumanos;


namespace CEA.Application.DTOs.Viaticos
{
    public class EmpleadoDto : IMapFrom<Empleado>
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty;
        public string Materno { get; set; } = string.Empty;
        public int Nivel { get; set; }
        public int Depto { get; set; }
        public int Obra { get; set; }
        public int DeptoPpto { get; set; }
        public int DeptoComi { get; set; }
        public int Municipio { get; set; }
        public string Activo { get; set; } = string.Empty;


    }
}
