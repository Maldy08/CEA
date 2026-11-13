using System;
using CEA.Domain.Exceptions;

namespace CEA.Domain.ValueObjects
{
    public readonly record struct ViaticoKey
    {
        public int Ejercicio { get; }
        public int Oficina { get; }
        public int NoViat { get; }

        public ViaticoKey(int ejercicio, int oficina, int noViat)
        {
            if (ejercicio < 2000)
                throw new DomainValidationException($"El ejercicio {ejercicio} no es válido.");
            if (oficina <= 0)
                throw new DomainValidationException("La oficina debe ser mayor a cero.");
            if (noViat <= 0)
                throw new DomainValidationException("El número de viático debe ser mayor a cero.");

            Ejercicio = ejercicio;
            Oficina = oficina;
            NoViat = noViat;
        }

        public string FormatearCodigoOficioComision(DateTime fecha) =>
            $"V{Oficina}-{NoViat}/{fecha:yy}";
    }
}
