namespace WebhookSample.Domain.Responses.Clients
{
    public class BaseClientResponse
    {
        /// <summary>
        /// Client id
        /// </summary>
        /// <example>9de6b3a1-a511-413b-86a6-2cf732d0b3cd</example>
        public Guid Id { get; set; }

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

        /// <summary>
        /// Client status
        /// </summary>
        /// <example>ACTIVE or INACTIVE</example>
        public string Status { get; set; }
    }
}
