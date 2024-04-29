using CEA.Application.DTOs.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IFormatoComisionRepository
    {
        Task<FormatoComisionDto> GetFormatoComisionByOficinaEjercicioNoviat(int oficina, int ejercicio, int noViat);
    }
}
