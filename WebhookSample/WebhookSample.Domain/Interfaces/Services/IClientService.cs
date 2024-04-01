using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;

namespace WebhookSample.Domain.Interfaces.Services
{
    public interface IClientService
    {
        Task<ClientCreatedResponse> CreateClient(CreateClientRequest newClient);
        Task<IEnumerable<GetClientResponse>> GetAllClients();
        Task<GetClientResponse> GetClientById(Guid id);
        Task<ClientUpdatedResponse> ChangeClientStatus(Guid id, bool status);
    }
}
