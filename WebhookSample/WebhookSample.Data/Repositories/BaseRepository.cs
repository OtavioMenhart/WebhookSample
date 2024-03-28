using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebhookSample.Data.Context;
using WebhookSample.Domain.Interfaces.Repositories;

namespace WebhookSample.Data.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ClientContext _dbContext;
        private DbSet<T> _dataSet;

        public BaseRepository(ClientContext dbContext)
        {
            _dbContext = dbContext;
            _dataSet = _dbContext.Set<T>();
        }

        public async Task<T> Get(Expression<Func<T, bool>> predicate)
        {
            return await _dataSet.SingleOrDefaultAsync(predicate);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dataSet.ToListAsync();
        }

        public async Task<T> InsertAsync(T item)
        {
            await _dataSet.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return item;
        }
    }
}
