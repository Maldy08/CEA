using CEA.Domain.Entities.Viaticos;


namespace CEA.Application.Interfaces.Repositories
{
    public interface IViaticoRepository
    {
        Task<List<Viatico>> GetAllViaticosByEjercicioAndOficina(int ejercicio, int oficina);
    }
}
