using System;
using System.Linq;

namespace CEA.Domain.Constants
{
    public static class BitacoraEstados
    {
        public const string Pendiente = "Pendiente";
        public const string EnProceso = "En Proceso";
        public const string Atendido = "Atendido";

        public static readonly string[] Todos = { Pendiente, EnProceso, Atendido };

        public static bool EsEstadoValido(string estado) =>
            Todos.Contains(estado, StringComparer.OrdinalIgnoreCase);
    }
}
