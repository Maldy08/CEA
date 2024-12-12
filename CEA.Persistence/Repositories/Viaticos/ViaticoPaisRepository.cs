using CEA.Application.DTOs.Viaticos;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoPaisRepository : IViaticoPaisRepository

    {
        private readonly ApplicationDbContext _dbContext;

        public ViaticoPaisRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<ViaticoPaisDto>> GetAll()
        {
            return await _dbContext.ViaticoPais.Select(x => new ViaticoPaisDto
            {
                IdPais = x.IdPais,
                Pais = x.Pais
            }).ToListAsync();
        }

        public async Task<ViaticoPaisDto> GetById(int id)
        {
           return await _dbContext.ViaticoPais.Where(x => x.IdPais == id).Select(x => new ViaticoPaisDto
            {
                IdPais = x.IdPais,
                Pais = x.Pais
            }).FirstOrDefaultAsync();
        }
    }
}
