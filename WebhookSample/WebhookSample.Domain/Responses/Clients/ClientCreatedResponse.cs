namespace WebhookSample.Domain.Responses.Clients
{
    public class ClientCreatedResponse : BaseClientResponse
    {
        /// <summary>
        /// Date and time of client creation
        /// </summary>
        /// <example>2000-01-01 00:00:00</example>
        public DateTime CreatedAt { get; set; }
    }
}
