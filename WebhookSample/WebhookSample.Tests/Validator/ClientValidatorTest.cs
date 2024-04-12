using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Service.Validators;

namespace WebhookSample.Tests.Validator
{
    public class ClientValidatorTest
    {
        [Fact]
        public async Task ValidateClient_NotValidRequest_ReturnsNotValid()
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
            var validationResult = clientValidator.Validate(request);

            //assert
            Assert.False(validationResult.IsValid);
        }
    }
}
