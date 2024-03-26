using WebhookSample.Domain.Enums;

namespace WebhookSample.Domain.Requests
{
    public class EventNotification
    {
        public string EventName { get; private set; }
        public DateTime EventDate { get; private set; }
        public object Info { get; private set; }

        public EventNotification(EventName eventName, object info)
        {
            EventName = eventName.ToString();
            EventDate = DateTime.UtcNow;
            Info = info;
        }
    }
}
