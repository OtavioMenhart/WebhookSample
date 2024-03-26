using MassTransit;
using WebhookSample.Domain.Requests;

namespace WebhookSample.API.Consumer
{
    public class EventConsumer : IConsumer<EventNotification>
    {
        public Task Consume(ConsumeContext<EventNotification> context)
        {
            var message = context.Message;
            return Task.CompletedTask;
        }
    }
}
