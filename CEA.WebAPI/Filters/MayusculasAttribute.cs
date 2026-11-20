using System.Collections;
using System.Globalization;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CEA.WebAPI.Filters
{
    // Regla del Sistema de Trámites (next-tramites-cea): todo lo capturado se guarda en MAYÚSCULAS.
    // Antes de entrar a la acción pasa a mayúsculas (es-MX, con acentos y Ñ) cada propiedad string de los objetos que
    // llegan en el cuerpo (commands/DTOs, también anidados y en listas). Los parámetros simples de ruta/query (ids,
    // años) no se tocan. Se pone por controlador; NO es global porque este backend lo comparten otros sistemas
    // (bitácoras, oficios, indicadores, checador). El front también lo hace (shared/server/cea.ts → ceaFetch).
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public sealed class MayusculasAttribute : ActionFilterAttribute
    {
        private static readonly CultureInfo Cultura = CultureInfo.GetCultureInfo("es-MX");
        private const int ProfundidadMaxima = 5;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var valor in context.ActionArguments.Values)
                Convertir(valor, 0);
        }

        private static void Convertir(object? objeto, int profundidad)
        {
            if (objeto == null || profundidad > ProfundidadMaxima || EsSimple(objeto.GetType())) return;

            if (objeto is IList lista)
            {
                for (var i = 0; i < lista.Count; i++)
                {
                    if (lista[i] is string s) { if (!lista.IsReadOnly) lista[i] = s.ToUpper(Cultura); }
                    else Convertir(lista[i], profundidad + 1);
                }
                return;
            }
            if (objeto is IEnumerable) return; // diccionarios y otras colecciones: no se usan en estos cuerpos

            foreach (var p in objeto.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (p.GetIndexParameters().Length > 0 || !p.CanRead) continue;
                var valor = p.GetValue(objeto);
                if (valor is string texto)
                {
                    // init-only (records) también tiene setter para reflexión.
                    if (p.CanWrite) p.SetValue(objeto, texto.ToUpper(Cultura));
                }
                else
                {
                    Convertir(valor, profundidad + 1);
                }
            }
        }

        private static bool EsSimple(Type t) =>
            t.IsPrimitive || t.IsEnum || t == typeof(string) || t == typeof(decimal) || t == typeof(DateTime)
            || t == typeof(DateTimeOffset) || t == typeof(TimeSpan) || t == typeof(Guid)
            || (Nullable.GetUnderlyingType(t) is Type n && EsSimple(n));
    }
}
