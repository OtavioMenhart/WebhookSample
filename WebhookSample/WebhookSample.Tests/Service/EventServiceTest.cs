using AutoFixture.Xunit2;
using MassTransit;
using NSubstitute;
using WebhookSample.Domain.Requests;
using WebhookSample.Service.Services;

namespace WebhookSample.Tests.Service
{
    public class EventServiceTest
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly ISendEndpoint _sendEndpoint;

        public EventServiceTest()
        {
            _sendEndpointProvider = Substitute.For<ISendEndpointProvider>();
            _sendEndpoint = Substitute.For<ISendEndpoint>();
        }

        [Theory, AutoData]
        public async Task SendEventNotification_Success(EventNotification notification)
        {
            // arrange
            _sendEndpointProvider.GetSendEndpoint(Arg.Any<Uri>()).Returns(_sendEndpoint);
            _sendEndpoint.Send(Arg.Any<object>()).Returns(Task.FromResult(true));

            // act
            var service = new EventService(_sendEndpointProvider);
            var result = await service.SendEventNotification(notification);

            // assert
            Assert.True(result);
        }
    }
}
