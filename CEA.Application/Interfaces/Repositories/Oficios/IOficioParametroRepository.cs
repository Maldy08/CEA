

using CEA.Application.DTOs.Oficios;

namespace CEA.Application.Interfaces.Repositories.Oficios
{
    public interface IOficioParametroRepository
    {
        Task<OficioParametroDto> GetOficioParametroByEjercicio(int ejercicio);
    }
}
