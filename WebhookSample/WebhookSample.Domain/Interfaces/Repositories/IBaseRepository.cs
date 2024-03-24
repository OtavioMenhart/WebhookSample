using WebhookSample.Domain.Entities;

namespace WebhookSample.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T item);
    }
}
