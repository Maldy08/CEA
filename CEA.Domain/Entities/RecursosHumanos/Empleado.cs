using CEA.Domain.Common;

namespace CEA.Domain.Entities.RecursosHumanos
{
    public class Empleado
    {

        public string Activo { get; set; } = string.Empty;
        public int Frecuencia { get; set; }
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Materno { get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty;
        public int IdPue { get; set; }
        public string DescripcionPuesto { get; set; } = string.Empty;
        public int DeptoUe { get; set; }
        public string DescripcionDepto { get; set; } = string.Empty;
        public int DeptoComi { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int Municipio { get; set; }
        public int Oficina { get; set; }
        public int Nivel { get; set; }
        public int LugarTrab { get; set; }
        public string Correo { get; set; } = string.Empty;

        //public int IdEmpleado { get; set; }
        //public string Nombre { get; set; } = string.Empty;
        //public string Paterno { get; set; } = string.Empty;
        //public string Materno { get; set; } = string.Empty;
        //public int Nivel { get; set; }
        //public int Depto { get; set; }
        public int Obra { get; set; }
        public int DeptoPpto { get; set; }
        //public int DeptoComi { get; set; }
        //public int Municipio { get; set; }
        //public string Activo { get; set; } = string.Empty;




    }
}
