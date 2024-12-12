using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoPartRepository : IViaticoPartRepository
    {
        private readonly IGenericRepository<ViaticoPart> _repository;

        public ViaticoPartRepository(IGenericRepository<ViaticoPart> repository)
        {
            _repository = repository;
        }
        public async Task<ViaticoPart> GetByOficinaEjercicioNoviatPartida(int oficina, int ejercicio, int noviat, int partida)
        {
            return await _repository.Entities.Where(x => x.Oficina == oficina && x.Ejercicio == ejercicio && x.NoViat == noviat && x.Partida == partida).FirstOrDefaultAsync();
        }

        public async Task<ViaticoPart> GetByOficinaEjercicioNoviat(int oficina, int ejercicio, int noviat)
        {
            return await _repository.Entities.Where(x => x.Oficina == oficina && x.Ejercicio == ejercicio && x.NoViat == noviat ).FirstOrDefaultAsync();
        }

        public async Task<int> InsertAsync(ViaticoPart viaticoPart)
        {
            await _repository.AddAsync(viaticoPart);
            return viaticoPart.Id;
        }
    }
}
