using System;
using System.Linq;

namespace CEA.Domain.Constants
{
    public static class TipoInvolucradoConstants
    {
        public const string Visualizador = "Visualizador";
        public const string Responsable = "Responsable";

        public static readonly string[] Todos = { Visualizador, Responsable };

        public static bool EsTipoValido(string tipo) =>
            Todos.Contains(tipo, StringComparer.OrdinalIgnoreCase);
    }
}
