

using CEA.Application.DTOs.Vehiculos;
using CEA.Application.Interfaces.Repositories.Vehiculos;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CEA.Persistence.Repositories.Vehiculos
{
    public class VsListaVehiculosRepository : IVsListaVehiculosRepository
    {
        private readonly ApplicationDbContext _context;

        public VsListaVehiculosRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<VsListaVehiculosDto>> GetAllVehiculos()
        { 

           return await _context.VsListaVehiculos.ToListAsync();
        }
    }
}
