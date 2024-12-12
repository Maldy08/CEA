using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoRepository
    {
        Task<List<Viatico>> GetAllViaticosByEjercicioAndOficina(int ejercicio, int oficina);
        Task<Viatico> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat);
        Task<List<Viatico>> GetAllByEjercicioDepto(int ejercicio, int empleado);
        Task<int> GetNoViat(int ejercicio, int oficina);

    }
}
