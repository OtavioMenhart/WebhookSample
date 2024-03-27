using System.Text.Json.Serialization;
using WebhookSample.Domain.Enums;

namespace WebhookSample.Domain.Requests
{
    public class EventNotification
    {
        public EventName EventName { get; private set; }
        public DateTime EventDate { get; private set; }
        public object Info { get; private set; }

        public EventNotification(EventName eventName, object info)
        {
            EventName = eventName;
            EventDate = DateTime.UtcNow;
            Info = info;
        }
    }
}
