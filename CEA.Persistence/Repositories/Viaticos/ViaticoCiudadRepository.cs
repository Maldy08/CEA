using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoCiudadRepository : IViaticoCiudadRepository
    {
        private readonly IGenericRepository<ViaticoCiudad> _genericRepository;

        public ViaticoCiudadRepository(IGenericRepository<ViaticoCiudad> genericRepository)
        {
            _genericRepository = genericRepository;
        }
    }
}
