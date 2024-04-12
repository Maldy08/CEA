
namespace CEA.Application.Common.Helpers
{
    public static class ViaticoImportePorDias
    {
        public static double CalcularImportePorDias(int dias, double importe)
        {
            return dias * importe;
        }
    }
}
