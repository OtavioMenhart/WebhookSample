using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Enums;
using WebhookSample.Domain.Interfaces.Repositories;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Domain.Requests;
using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;

namespace WebhookSample.Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IValidator<BaseClientRequest> _clientValidator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly IBaseRepository<ClientHistory> _clientHistoryRepository;
        private readonly IEventService _eventService;

        public ClientService(IValidator<BaseClientRequest> clientValidator, 
            IMapper mapper, 
            IBaseRepository<Client> clientRepository, 
            IEventService eventService, 
            IBaseRepository<ClientHistory> clientHistoryRepository)
        {
            _clientValidator = clientValidator;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _eventService = eventService;
            _clientHistoryRepository = clientHistoryRepository;
        }

        public async Task<ClientUpdatedResponse> ChangeClientStatus(Guid id, bool status)
        {
            var client = await SearchClient(id);

            EventName clientEvent = status ? EventName.CLIENT_ACTIVE : EventName.CLIENT_INACTIVE;

            client.ChangeStatus(client, status);
            await _clientRepository.Update(client);

            client.AddHistory(client, clientEvent);
            await _clientHistoryRepository.InsertAsync(client.Histories.FirstOrDefault());

            await _eventService.SendEventNotification(new EventNotification(clientEvent.ToString(), client));

            return _mapper.Map<ClientUpdatedResponse>(client);
        }

        public async Task<ClientCreatedResponse> CreateClient(CreateClientRequest newClient)
        {
            await ValidateClientRequest(newClient);

            EventName clientEvent = EventName.CLIENT_CREATED;
            var client = _mapper.Map<Client>(newClient);
            client.AddHistory(client, clientEvent);
            var clientAdded = await _clientRepository.InsertAsync(client);

            await _eventService.SendEventNotification(new EventNotification(clientEvent.ToString(), clientAdded));
            return _mapper.Map<ClientCreatedResponse>(clientAdded);
        }        

        public async Task<IEnumerable<GetClientResponse>> GetAllClients()
        {
            var clients = await _clientRepository.GetAll();
            return _mapper.Map<IEnumerable<GetClientResponse>>(clients);
        }

        public async Task<GetClientResponse> GetClientById(Guid id)
        {
            var client = await SearchClient(id);
            return _mapper.Map<GetClientResponse>(client);
        }

        public async Task<ClientUpdatedResponse> UpdateClientInformations(Guid id, UpdateClientRequest request)
        {
            var client = await SearchClient(id);

            if (client.Status == "INACTIVE")
                throw new ValidationException(new List<ValidationFailure> 
                { 
                    new ValidationFailure("Status", "You can't update an inactive client, please active this client first") 
                });

            await ValidateClientRequest(request);

            client.UpdateInfos(client, request);
            await _clientRepository.Update(client);

            EventName clientEvent = EventName.CLIENT_UPDATED;
            client.AddHistory(client, clientEvent);
            await _clientHistoryRepository.InsertAsync(client.Histories.FirstOrDefault());

            await _eventService.SendEventNotification(new EventNotification(clientEvent.ToString(), client));

            return _mapper.Map<ClientUpdatedResponse>(client);
        }

        private async Task<Client> SearchClient(Guid id)
        {
            var client = await _clientRepository.Get(x => x.Id == id);
            if (client == null)
                throw new KeyNotFoundException($"Client not found");
            return client;
        }

        private async Task ValidateClientRequest(BaseClientRequest newClient)
        {
            var validationResult = await _clientValidator.ValidateAsync(newClient);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);
        }
    }
}
