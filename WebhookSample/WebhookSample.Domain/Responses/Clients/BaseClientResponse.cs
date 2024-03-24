namespace WebhookSample.Domain.Responses.Clients
{
    public class BaseClientResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Email { get; set; }
    }
}
