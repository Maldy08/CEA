using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoEstadoRepository : IViaticoEstadoRepository
    {
        private readonly IGenericRepository<ViaticoEstado> _genericRepository;

        public ViaticoEstadoRepository(IGenericRepository<ViaticoEstado> genericRepository)
        {
            _genericRepository = genericRepository;
        }
    }
}
