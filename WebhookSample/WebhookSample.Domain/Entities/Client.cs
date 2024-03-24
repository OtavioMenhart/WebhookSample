namespace WebhookSample.Domain.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public  string Email { get; set; }
    }
}
