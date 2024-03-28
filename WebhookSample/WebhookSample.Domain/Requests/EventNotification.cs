namespace WebhookSample.Domain.Requests
{
    public class EventNotification
    {
        public string EventName { get; private set; }
        public DateTime EventDate { get; private set; }
        public object Info { get; private set; }

        public EventNotification(string eventName, object info)
        {
            EventName = eventName;
            EventDate = DateTime.UtcNow;
            Info = info;
        }
    }
}
