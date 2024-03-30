using AutoMapper;
using FluentValidation;
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
        private readonly IEventService _eventService;

        public ClientService(IValidator<BaseClientRequest> clientValidator, IMapper mapper, IBaseRepository<Client> clientRepository, IEventService eventService)
        {
            _clientValidator = clientValidator;
            _mapper = mapper;
            _clientRepository = clientRepository;
            _eventService = eventService;
        }

        public async Task<ClientCreatedResponse> CreateClient(CreateClientRequest newClient)
        {
            var validationResult = await _clientValidator.ValidateAsync(newClient);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

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
            var client = await _clientRepository.Get(x => x.Id == id);
            if (client == null)
                throw new KeyNotFoundException($"Client not found");

            return _mapper.Map<GetClientResponse>(client);
        }
    }
}
