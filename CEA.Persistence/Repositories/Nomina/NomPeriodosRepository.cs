using CEA.Application.DTOs.Nomina;
using CEA.Application.Interfaces.Repositories.Nomina;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Nomina
{
    public class NomPeriodosRepository : INomPeriodosRepository
    {

        private readonly ApplicationDbContext _context;

        public NomPeriodosRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<NomPeriodosDto>> GetNomPeriodosByTipoNomEjercicioAsync(int tipoNom, int ejercicio)
        {
            return await _context.NomPeriodos
                .Where(x => x.TipoNom == tipoNom && x.AnoProceso == ejercicio)
                .Select(x => new NomPeriodosDto
                {
                    TipoNom = x.TipoNom,
                    PerNom = x.PerNom,
                    FincDes = x.FincDes,
                    FinciHas = x.FinciHas,
                    AnoProceso = x.AnoProceso
                })
                .OrderBy(x => x.PerNom)
                .ToListAsync();
        }
    }
}
