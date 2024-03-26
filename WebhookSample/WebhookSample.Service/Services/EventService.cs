using MassTransit;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Domain.Requests;

namespace WebhookSample.Service.Services
{
    public class EventService : IEventService
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public EventService(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task<bool> SendEventNotification(EventNotification notification)
        {
            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:clients"));
            await sendEndpoint.Send(notification);
            return true;
        }
    }
}
