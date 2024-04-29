using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;
using Microsoft.EntityFrameworkCore;


namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoRepository : IViaticoRepository
    {

        private readonly IGenericRepository<Viatico> _repository;

        public ViaticoRepository(IGenericRepository<Viatico> repository)
        {
            _repository = repository;
        }

        public async Task<List<Viatico>> GetAllByEjercicioDepto(int ejercicio, int empleado)
        {
            return await _repository.Entities.Where(x => x.Ejercicio == ejercicio && x.NoEmp == empleado).ToListAsync();
        }

        public async Task<Viatico> GetAllByEjercicioOficinaNoviat(int ejercicio, int oficina, int noviat)
        {
            var result = await _repository.Entities.Where(x => x.Ejercicio == ejercicio && x.Oficina == oficina && x.NoViat == noviat).FirstOrDefaultAsync();
            return result;
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
