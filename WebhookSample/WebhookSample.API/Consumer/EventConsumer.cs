using MassTransit;
using WebhookSample.Domain.Requests;
using WebhookSample.Service.External;

namespace WebhookSample.API.Consumer
{
    public class EventConsumer : IConsumer<EventNotification>
    {
        private readonly IApiClientService _apiClientService;

        public EventConsumer(IApiClientService apiClientService)
        {
            _apiClientService = apiClientService;
        }

        public Task Consume(ConsumeContext<EventNotification> context)
        {
            var message = context.Message;
            var result = _apiClientService.SendClientNotification(message);
            result.Wait();

            return Task.CompletedTask;
        }
    }
}
