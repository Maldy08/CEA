using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;


namespace CEA.Persistence.Repositories
{
    public class ViaticoRepository : IViaticoRepository
    {

        private readonly IGenericRepository<Viatico> _repository;

        public ViaticoRepository(IGenericRepository<Viatico> repository)
        {
            _repository = repository;
        }

        public Task<List<Viatico>> GetAllByEjercicioDepto(int ejercicio, int empleado)
        {
           return _repository.Entities.Where(x => x.Ejercicio == ejercicio && x.NoEmp == empleado).ToListAsync();
        }

        public Task<List<Viatico>> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Viatico>> GetAllViaticosByEjercicioAndOficina(int ejercicio, int oficina)
        {
           return await _repository.Entities.Where(x => x.Ejercicio == ejercicio && x.Oficina == oficina).ToListAsync();
        }

        public async Task<int> GetNoViat(int ejercicio, int oficina)
        {
            var result = 0;
            var conteo = await _repository.Entities.Where(x => x.Ejercicio == ejercicio && x.Oficina == oficina).CountAsync();
            if (conteo > 0)
            {
                result = _repository.Entities.Where(x => x.Ejercicio == ejercicio && x.Oficina == oficina).Max(x => x.NoViat) + 1;
            }
            else
            {
                result = 1;
            }

            return result;
        }
    }
}
