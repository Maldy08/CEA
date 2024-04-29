using CEA.Application.Interfaces.Repositories;
using CEA.Application.Interfaces.Repositories.Viaticos;
using CEA.Domain.Entities.Viaticos;

namespace CEA.Persistence.Repositories.Viaticos
{
    public class ViaticoPaisRepository : IViaticoPaisRepository
    {
        private readonly IGenericRepository<ViaticoPais> _genericRepository;

        public ViaticoPaisRepository(IGenericRepository<ViaticoPais> genericRepository)
        {
            _genericRepository = genericRepository;
        }
    }
}
