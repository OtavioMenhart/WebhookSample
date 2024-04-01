namespace WebhookSample.Domain.Responses.Clients
{
    public class ClientUpdatedResponse : BaseClientResponse
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
