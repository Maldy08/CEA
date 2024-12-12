

using CEA.Application.DTOs;
using CEA.Application.Interfaces.Repositories;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories
{
    public class DeptoRepository : IDeptoRepository
    {
        private readonly ApplicationDbContext _context;

        public DeptoRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public  async Task<DeptoUeDto> GetDeptoByIdAsync(int id)
        {

            return await _context.DeptoUe.Select(d => new DeptoUeDto
            {
                IdCea = d.IdCea,
                Accion = d.Accion,
                AgrupaDir = d.AgrupaDir,
                AgrupaPoa = d.AgrupaPoa,
                Descripcion = d.Descripcion,
                EmpRespon = d.EmpRespon,
                IdReporta = d.IdReporta,
                IdShpoa = d.IdShpoa,
                Meta = d.Meta,
                Nivel = d.Nivel,
                Oficial = d.Oficial,
                Prog = d.Prog

            }).FirstOrDefaultAsync(d => d.IdCea == id);
        }

        public async Task<IEnumerable<DeptoUeDto>> GetDeptosAsync()
        {

            return await _context.DeptoUe.Select(d => new DeptoUeDto
            {
                IdCea = d.IdCea,
                Accion = d.Accion,
                AgrupaDir = d.AgrupaDir,
                AgrupaPoa = d.AgrupaPoa,
                Descripcion = d.Descripcion,
                EmpRespon = d.EmpRespon,
                IdReporta = d.IdReporta,
                IdShpoa = d.IdShpoa,
                Meta = d.Meta,
                Nivel = d.Nivel,
                Oficial = d.Oficial,
                Prog = d.Prog

            }).OrderBy(d => d.IdCea).ToListAsync();
        }
    }
}
