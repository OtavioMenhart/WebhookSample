namespace WebhookSample.Domain.Responses.Clients
{
    public class GetClientResponse : BaseClientResponse
    {
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
