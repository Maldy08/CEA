using CEA.Application.DTOs.Vehiculos;
using CEA.Application.Interfaces.Repositories.Vehiculos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace CEA.Persistence.Repositories.Vehiculos
{
    public class VsWtVehiculosRepository : IVsWtVehiculosRepository
    {

        private readonly ApplicationDbContext _context;

        public VsWtVehiculosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<VsWtVehiculosDto> GetVehiculoByNoEconomico(int noEconomico)
        {
            return await _context.VsWtVehiculos.FirstOrDefaultAsync(x => x.Numero == noEconomico);
        }

        public async Task<List<VsWtVehiculosDto>> GetVehiculos()
        {
            return await _context.VsWtVehiculos.ToListAsync();
        }
    }
}
