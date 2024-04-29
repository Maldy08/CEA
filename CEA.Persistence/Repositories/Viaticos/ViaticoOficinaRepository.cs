using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoOficinaRepository : IViaticoOficinaRepository
    {
        private readonly IGenericRepository<ViaticoOfi> _genericRepository;

        public ViaticoOficinaRepository(IGenericRepository<ViaticoOfi> genericRepository)
        {
            _genericRepository = genericRepository;
        }
    }
}
