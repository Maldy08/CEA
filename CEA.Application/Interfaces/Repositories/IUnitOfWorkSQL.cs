

using CEA.Domain.Common;

namespace CEA.Application.Interfaces.Repositories
{
    public interface IUnitOfWorkSQL
    {
        IGenericRepositorySQL<T> Repository<T>() where T : BaseAuditableEntity;
        Task<int> Save(CancellationToken cancellationToken);
        Task<int> SaveAndRemoveCache(CancellationToken cancellationToken, params string[] cacheKeys);
        Task Rollback();
    }
}
