using AutoFixture.Xunit2;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System.Net;
using WebhookSample.API.Controllers;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;
using WebhookSample.Tests.Custom;

namespace WebhookSample.Tests.API
{
    public class ClientControllerTests
    {
        private readonly IClientService _clientService;

        public ClientControllerTests()
        {
            _clientService = Substitute.For<IClientService>();
        }

        [Theory, CustomAutoData]
        public async Task GetAllClients_ReturnsListClients_Ok(IEnumerable<GetClientResponse> allClients)
        {
            // arrange
            _clientService.GetAllClients().Returns(allClients);

            var controller = new ClientController(_clientService);

            // act
            var result = await controller.Get();

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(allClients, okResult.Value);
            Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);
        }

        [Fact]
        public async Task GetAllClients_ReturnsNullListClients_NoContent()
        {
            // arrange
            _clientService.GetAllClients().ReturnsNull();

            var controller = new ClientController(_clientService);

            // act
            var result = await controller.Get();

            // assert
            Assert.IsType<NoContentResult>(result);

            var noContentResult = result as NoContentResult;
            Assert.NotNull(noContentResult);
            Assert.True(noContentResult.StatusCode == (int)HttpStatusCode.NoContent);
        }

        [Theory, CustomAutoData]
        public async Task GetClientById_ReturnsClient_Ok(Guid id, GetClientResponse client)
        {
            // arrange
            _clientService.GetClientById(Arg.Any<Guid>()).Returns(client);

            var controller = new ClientController(_clientService);

            // act
            var result = await controller.GetById(id);

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(client, okResult.Value);
            Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);
        }

        [Theory, CustomAutoData]
        public async Task PostClient_ReturnsClientAdded_Ok(CreateClientRequest newClient, ClientCreatedResponse clientCreated)
        {
            // arrange
            _clientService.CreateClient(Arg.Any<CreateClientRequest>()).Returns(clientCreated);

            var controller = new ClientController(_clientService);

            // act
            var result = await controller.Post(newClient);

            // assert
            Assert.NotNull(result);

            var createdResult = result as ObjectResult;
            Assert.NotNull(createdResult);
            Assert.True(createdResult.StatusCode == (int)HttpStatusCode.Created);
        }

        [Theory, CustomAutoData]
        public async Task PutClient_ReturnsClientUpdated_Ok(Guid id, UpdateClientRequest updateClient, ClientUpdatedResponse clientUpdated)
        {
            // arrange
            _clientService.UpdateClientInformations(Arg.Any<Guid>(), Arg.Any<UpdateClientRequest>()).Returns(clientUpdated);

            var controller = new ClientController(_clientService);

            // act
            var result = await controller.Put(id, updateClient);

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);
        }

        [Theory, CustomAutoData]
        public async Task ChangeClientStatus_ReturnsClientUpdated_Ok(Guid id, bool status, ClientUpdatedResponse clientUpdated)
        {
            // arrange
            _clientService.ChangeClientStatus(Arg.Any<Guid>(), Arg.Any<bool>()).Returns(clientUpdated);

            var controller = new ClientController(_clientService);

            // act
            var result = await controller.ChangeStatus(id, status);

            // assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result);

            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.True(okResult.StatusCode == (int)HttpStatusCode.OK);
        }
    }
}
