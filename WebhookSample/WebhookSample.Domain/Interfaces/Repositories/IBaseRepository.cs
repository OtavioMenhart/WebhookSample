using WebhookSample.Domain.Entities;

namespace WebhookSample.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> InsertAsync(T item);
    }
}
