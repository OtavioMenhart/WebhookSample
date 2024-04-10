namespace WebhookSample.Domain.Requests.Clients
{
    public class BaseClientRequest
    {
        /// <summary>
        /// Client name
        /// </summary>
        /// <example>Full name</example>
        public string Name { get; set; }

        /// <summary>
        /// Birth date
        /// Format "yyyy-MM-dd"
        /// </summary>
        /// <example>2000-01-01</example>
        public DateOnly BirthDate { get; set; }

        /// <summary>
        /// Client email
        /// </summary>
        /// <example>clientemail@gmail.com</example>
        public string Email { get; set; }
    }
}
