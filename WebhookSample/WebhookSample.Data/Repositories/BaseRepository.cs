using Microsoft.EntityFrameworkCore;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Interfaces.Repositories;

namespace WebhookSample.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _dbContext;
        private DbSet<T> _dataSet;

        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dataSet = _dbContext.Set<T>();
        }

        public async Task<T> InsertAsync(T item)
        {
            await _dataSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
