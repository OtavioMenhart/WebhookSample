using System.Linq.Expressions;

namespace WebhookSample.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> InsertAsync(T item);
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> predicate);
    }
}
