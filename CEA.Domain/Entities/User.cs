

using CEA.Domain.Common;

namespace CEA.Domain.Entities
{
    public class User : BaseAuditableEntity
    {

        public int Usuario { get; set; }
        public string Login { get; set; } = string.Empty;
        public string Pass { get; set; } = string.Empty;
        public bool? Activo { get; set; }
        public bool? Compras { get; set; }
        public int? ComprasNivel { get; set; }
        public bool? Almacen { get; set; }
        public int? AlmacenNivel { get; set; }
        public bool? Activos { get; set; }
        public int? ActivosNivel { get; set; }
        public bool? Contabilidad { get; set; }
        public int? ContabilidadNivel { get; set; }
        public bool? Presupuestos { get; set; }
        public int? PresupuestosNivel { get; set; }
        public bool? Nominas { get; set; }
        public int? NominasNivel { get; set; }
        public int Depto { get; set; }
        public int NoEmpleado { get; set; }
        public int? Bd { get; set; }
        public bool? Caja { get; set; }
        public int? CajaNivel { get; set; }
        public string Polnom { get; set; } = string.Empty;
        public bool? Viaticos { get; set; }
        public int? ViaticosNivel { get; set; }
        public bool? Vales { get; set; }
        public int? ValesNivel { get; set; }
        public string DeptoDescripcion { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public int IdPue { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public int Municipio { get; set; }
        public int Oficina { get; set; }

        public int? Oficios { get; set; }
        public int? OficiosNivel { get; set; }

        public string Nombre {  get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty; 
        public string Materno {  get; set; } = string.Empty;


    }
}
