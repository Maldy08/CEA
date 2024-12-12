using CEA.Domain.Entities.Viaticos;

namespace CEA.Application.Interfaces.Repositories.Viaticos
{
    public interface IViaticoPartRepository
    {
        //  Task<List<ViaticoPart>> GetAllByNoViatAndEjercicio(int noViat, int ejercicio);
        Task<ViaticoPart> GetByOficinaEjercicioNoviatPartida(int oficina, int ejercicio, int noviat, int partida);
        
        Task<ViaticoPart> GetByOficinaEjercicioNoviat(int oficina, int ejercicio, int noviat);
        Task<int> InsertAsync(ViaticoPart viaticoPart);



    }
}
