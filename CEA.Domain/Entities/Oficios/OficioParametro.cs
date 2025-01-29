using CEA.Domain.Common;


namespace CEA.Domain.Entities.Oficios
{
    public class OficioParametro :  BaseAuditableEntity
    {
        
        public int Ejercicio { get; set; }
        public int NextFRec { get; set; }
        public int NextFEnv { get; set; }
        public int NextFXexp { get; set; }
    }
}
