using AutoMapper;
using FluentValidation;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Interfaces.Repositories.Clients;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;

namespace WebhookSample.Service.Services
{
    public class ClientService : IClientService
    {
        private readonly IValidator<BaseClientRequest> _clientValidator;
        private readonly IMapper _mapper;
        private readonly IClientRepository _clientRepository;

        public ClientService(IValidator<BaseClientRequest> clientValidator, IMapper mapper, IClientRepository clientRepository)
        {
            _clientValidator = clientValidator;
            _mapper = mapper;
            _clientRepository = clientRepository;
        }

        public async Task<ClientCreatedResponse> CreateClient(CreateClientRequest newClient)
        {
            var validationResult = await _clientValidator.ValidateAsync(newClient);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            var client = _mapper.Map<Client>(newClient);
            var clientAdded = await _clientRepository.InsertAsync(client);
            return _mapper.Map<ClientCreatedResponse>(clientAdded);
        }
    }
}
