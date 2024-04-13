using Bogus;
using WebhookSample.Data.Repositories;
using WebhookSample.Domain.Entities;
using WebhookSample.Domain.Interfaces.Repositories;
using WebhookSample.Domain.Requests.Clients;

namespace WebhookSample.Tests.Repository
{
    public class RepositoryTest : BaseTest
    {
        private readonly Faker<Client> _clientFaker;
        private readonly IBaseRepository<Client> _clientRepository;
        public RepositoryTest()
        {
            _clientFaker = new Faker<Client>()
                .RuleFor(u => u.Name, (f, u) => f.Name.FullName())
                .RuleFor(u => u.BirthDate, (f, u) => f.Date.PastDateOnly())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email())
                .RuleFor(u => u.Status, (f, u) => f.Random.Bool() ? "ACTIVE" : "INACTIVE");

            _clientRepository = new BaseRepository<Client>(_context);
        }

        [Fact]
        public async Task AllBaseMethods_Success()
        {
            var client = _clientFaker.Generate();

            var clientCreated = await _clientRepository.InsertAsync(client);
            Assert.True(clientCreated == client);

            var getClientAdded = await _clientRepository.Get(x => x.Id == clientCreated.Id);
            Assert.True(getClientAdded == clientCreated);

            var secondClient = _clientFaker.Generate();
            await _clientRepository.InsertAsync(secondClient);

            var listClients = await _clientRepository.GetAll();
            Assert.True(listClients.Count() == 2);

            var updateClient = _clientFaker.Generate();
            secondClient.UpdateInfos(secondClient, new UpdateClientRequest
            {
                BirthDate = updateClient.BirthDate,
                Email = updateClient.Email,
                Name = updateClient.Name,
            });

            await _clientRepository.Update(secondClient);
            secondClient = await _clientRepository.Get(x => x.Id == secondClient.Id);

            Assert.Multiple(() =>
            {
                Assert.NotEqual(secondClient.Id, updateClient.Id);
                Assert.Equal(updateClient.Name, secondClient.Name);
                Assert.Equal(updateClient.Email, secondClient.Email);
                Assert.Equal(updateClient.BirthDate, secondClient.BirthDate);
            });
        }
    }
}
