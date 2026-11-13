using CEA.Domain.Exceptions;

namespace CEA.Domain.ValueObjects
{
    public readonly record struct OficioKey
    {
        public int Ejercicio { get; }
        public int Folio { get; }
        public int Eor { get; }

        public OficioKey(int ejercicio, int folio, int eor)
        {
            if (ejercicio < 2000)
                throw new DomainValidationException($"El ejercicio {ejercicio} no es válido.");
            if (folio < 0)
                throw new DomainValidationException("El folio no puede ser negativo.");
            if (eor is < 1 or > 3)
                throw new DomainValidationException($"El tipo EOR ({eor}) debe ser 1 (Expedido), 2 (Recibido) o 3 (Por Expedir).");

            Ejercicio = ejercicio;
            Folio = folio;
            Eor = eor;
        }

        public string ObtenerCarpetaDestino() =>
            Eor == 1 ? "oficios-expedidos" : "oficios-recibidos";

        public string ObtenerNombreArchivoPdf() =>
            $"{Ejercicio}-{Eor}-{Folio}.pdf";

        public string ObtenerRutaRelativaPdf() =>
            $"{ObtenerCarpetaDestino()}/{ObtenerNombreArchivoPdf()}";
    }
}
