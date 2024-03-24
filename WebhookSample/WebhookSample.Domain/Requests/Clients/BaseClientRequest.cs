namespace WebhookSample.Domain.Requests.Clients
{
    public class BaseClientRequest
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
    }
}
