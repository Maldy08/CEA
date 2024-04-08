using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.Interfaces.Repositories
{
    public interface IViaticoRepository
    {
        Task<List<Viatico>> GetAllViaticosByEjercicioAndOficina(int ejercicio, int oficina);
        Task<List<Viatico>> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat);
        Task<List<Viatico>> GetAllByEjercicioDepto(int ejercicio, int empleado);
        Task<int> GetNoViat(int ejercicio, int oficina);
    }
}
