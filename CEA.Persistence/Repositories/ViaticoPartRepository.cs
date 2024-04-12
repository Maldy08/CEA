

using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories
{
    public class ViaticoPartRepository : IViaticoPartRepository
    {
        private readonly IGenericRepository<ViaticoPart> _repository;
        
        public ViaticoPartRepository(IGenericRepository<ViaticoPart> repository)
        {
            this._repository = repository;
        }
        public async Task<ViaticoPart> GetByOficinaEjercicioNoviatPartida(int oficina, int ejercicio, int noviat, int partida)
        {
            return await _repository.Entities.Where(x => x.Oficina == oficina && x.Ejercicio == ejercicio && x.NoViat == noviat && x.Partida == partida).FirstOrDefaultAsync();
        }
    }
}
