namespace WebhookSample.Domain.Responses.Clients
{
    public class ClientUpdatedResponse : BaseClientResponse
    {
        /// <summary>
        /// Date and time of client creation
        /// </summary>
        /// <example>2000-01-01 00:00:00</example>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date and time of client update
        /// </summary>
        /// <example>2000-01-01 00:00:00</example>
        public DateTime UpdatedAt { get; set; }
    }
}
