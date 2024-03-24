using Microsoft.EntityFrameworkCore;
using WebhookSample.Data.Context;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Interfaces.Repositories.Clients;

namespace WebhookSample.Data.Repositories.Clients
{
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        private readonly DbSet<Client> _dataSet;
        public ClientRepository(ClientContext dbContext) : base(dbContext)
        {
            _dataSet = dbContext.Set<Client>();
        }
    }
}
