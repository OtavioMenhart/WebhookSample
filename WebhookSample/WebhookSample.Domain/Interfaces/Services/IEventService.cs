using WebhookSample.Domain.Requests;

namespace WebhookSample.Domain.Interfaces.Services
{
    public interface IEventService
    {
        Task<bool> SendEventNotification(EventNotification notification);
    }
}
