using AutoFixture.Xunit2;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Linq.Expressions;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Interfaces.Repositories;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Domain.Requests;
using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;
using WebhookSample.Service.Services;
using WebhookSample.Service.Validators;
using WebhookSample.Tests.Custom;

namespace WebhookSample.Tests.Service
{
    public class ClientServiceTest
    {
        private readonly IValidator<BaseClientRequest> _clientValidator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository<Client> _clientRepository;
        private readonly IBaseRepository<ClientHistory> _clientHistoryRepository;
        private readonly IEventService _eventService;

        public ClientServiceTest()
        {
            _clientValidator = Substitute.For<IValidator<BaseClientRequest>>();
            _mapper = Substitute.For<IMapper>();
            _clientRepository = Substitute.For<IBaseRepository<Client>>();
            _clientHistoryRepository = Substitute.For<IBaseRepository<ClientHistory>>();
            _eventService = Substitute.For<IEventService>();
        }

        [Theory, CustomAutoData]
        public async Task ChangeClientStatus_ValidId_ReturnsClientUpdated(Client client, ClientHistory history,
            ClientUpdatedResponse response)
        {
            // arrange
            bool eventResult = true;
            _clientRepository.Get(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);
            _clientRepository.Update(Arg.Any<Client>()).Returns(Task.FromResult(true));
            _clientHistoryRepository.InsertAsync(Arg.Any<ClientHistory>()).Returns(history);
            _eventService.SendEventNotification(Arg.Any<EventNotification>()).Returns(eventResult);
            _mapper.Map<ClientUpdatedResponse>(Arg.Any<Client>()).Returns(response);

            var service = new ClientService(_clientValidator, _mapper, 
                                            _clientRepository, _eventService, _clientHistoryRepository);

            // act
            var resultActive = await service.ChangeClientStatus(Guid.NewGuid(), true);

            // assert
            Assert.NotNull(resultActive);

            // act
            var resultInactive = await service.ChangeClientStatus(Guid.NewGuid(), false);

            // assert
            Assert.NotNull(resultInactive);
        }

        [Theory, CustomAutoData]
        public async Task CreateClient_ValidRequest_ReturnsClientCreated(CreateClientRequest request, Client client,
            ClientCreatedResponse response)
        {
            // arrange
            bool eventResult = true;
            _clientValidator.ValidateAsync(Arg.Any<BaseClientRequest>()).Returns(new ValidationResult());
            _mapper.Map<Client>(Arg.Any<CreateClientRequest>()).Returns(client);
            _clientRepository.InsertAsync(Arg.Any<Client>()).Returns(client);
            _eventService.SendEventNotification(Arg.Any<EventNotification>()).Returns(eventResult);
            _mapper.Map<ClientCreatedResponse>(Arg.Any<Client>()).Returns(response);

            // act
            var service = new ClientService(_clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            var result = await service.CreateClient(request);

            //assert
            Assert.NotNull(result);
            Assert.IsType<ClientCreatedResponse>(result);
        }

        [Fact]
        public async Task CreateClient_NotValidRequest_ThrowsValidationException()
        {
            // arrange
            CreateClientRequest request = new CreateClientRequest
            {
                BirthDate = new DateOnly(),
                Name = "",
                Email = ""
            };
            var clientValidator = new ClientValidator();

            // act
            var service = new ClientService(clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            //assert
            Assert.ThrowsAsync<ValidationException>(async () => await service.CreateClient(request));
        }

        [Theory, CustomAutoData]
        public async Task GetAllClients_ReturnsClients(IEnumerable<Client> clients, 
            IEnumerable<GetClientResponse> response)
        {
            // arrange
            _clientRepository.GetAll().Returns(clients);
            _mapper.Map<IEnumerable<GetClientResponse>>(Arg.Any<IEnumerable<Client>>()).Returns(response);

            // act
            var service = new ClientService(_clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            var result = await service.GetAllClients();

            // assert
            Assert.NotNull(result);
            Assert.True(result.Any());
        }

        [Theory, CustomAutoData]
        public async Task GetClientById_ReturnsClient(Client client, GetClientResponse response)
        {
            // arrange
            _clientRepository.Get(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);
            _mapper.Map<GetClientResponse>(Arg.Any<Client>()).Returns(response);

            // act
            var service = new ClientService(_clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            var result = await service.GetClientById(Guid.NewGuid());

            // assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetClientById_NotFound_ThrowsKeyNotFoundException()
        {
            // arrange
            _clientRepository.Get(Arg.Any<Expression<Func<Client, bool>>>()).ReturnsNull();

            // act
            var service = new ClientService(_clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            // assert
            Assert.ThrowsAsync<KeyNotFoundException>(async () => await service.GetClientById(Guid.NewGuid()));
        }

        [Theory, CustomAutoData]
        public async Task UpdateClientInformations_InactiveClient_ThrowsValidationException(UpdateClientRequest request)
        {
            // arrange
            var client = new Client("inactive", new DateOnly(), "email", false, null);
            _clientRepository.Get(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);

            // act
            var service = new ClientService(_clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            // assert
            Assert.ThrowsAsync<ValidationException>(async () => await service.UpdateClientInformations(Guid.NewGuid(), request));
        }

        [Theory, CustomAutoData]
        public async Task UpdateClientInformations_ActiveClient_ReturnsClientUpdated(UpdateClientRequest request,
            ClientHistory history, ClientUpdatedResponse response)
        {
            // arrange
            bool eventResult = true;
            var client = new Client("active", new DateOnly(), "email", true, null);
            _clientRepository.Get(Arg.Any<Expression<Func<Client, bool>>>()).Returns(client);
            _clientValidator.ValidateAsync(Arg.Any<BaseClientRequest>()).Returns(new ValidationResult());
            _clientRepository.Update(Arg.Any<Client>()).Returns(Task.FromResult(true));
            _clientHistoryRepository.InsertAsync(Arg.Any<ClientHistory>()).Returns(history);
            _eventService.SendEventNotification(Arg.Any<EventNotification>()).Returns(eventResult);
            _mapper.Map<ClientUpdatedResponse>(Arg.Any<Client>()).Returns(response);


            // act
            var service = new ClientService(_clientValidator, _mapper,
                                            _clientRepository, _eventService, _clientHistoryRepository);

            var result = await service.UpdateClientInformations(Guid.NewGuid(), request);

            // assert
            Assert.NotNull(result);
            Assert.True(result == response);
        }
    }
}
