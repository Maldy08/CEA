using CEA.Domain.Exceptions;

namespace CEA.Domain.Constants
{
    public static class ViaticoImporte
    {
        public const double ImporteViaticoDirectorDentroEstado = 350;
        public const double ImporteViaticoDirectorFueraEstado = 600;

        public const double ImporteViaticoJefeDentroEstado = 300;
        public const double ImporteViaticoJefeFueraEstado = 500;

        public const double ImporteViaticoEmpleadoDentroEstado = 270;
        public const double ImporteViaticoEmpleadoFueraEstado = 400;

        public static double CalcularImportePorDias(int dias, double importeDiario)
        {
            if (dias < 0)
                throw new DomainValidationException("El número de días no puede ser negativo.");
            if (importeDiario < 0)
                throw new DomainValidationException("El importe diario no puede ser negativo.");

            return dias * importeDiario;
        }
    }
}
