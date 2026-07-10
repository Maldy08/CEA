namespace CEA.Application.DTOs.IndiArct
{
    // Item de entrada para el guardado por lote de energía.
    public class EnergiaMesItem
    {
        public int Mes { get; set; }
        public decimal Volumenes { get; set; }
        public decimal Kwh { get; set; }
        public decimal Costo { get; set; }
    }
}
