using CEA.Application.Interfaces.Repositories;
using CEA.Domain.Common;
using CEA.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace CEA.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseAuditableEntity
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();
        

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }


        public Task DeleteAsync(T entity)
        {

            try
            {
                T exist = _dbContext.Set<T>().Find(entity.Id);
                _dbContext.Entry(exist).State = EntityState.Deleted;
                _dbContext.Set<T>().Remove(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                return await _dbContext.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public Task UpdateAsync(T entity)
        {
            T exist = _dbContext.Set<T>().Find(entity.Id);
            try
            {
                _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return Task.CompletedTask;
        }
    }
}
