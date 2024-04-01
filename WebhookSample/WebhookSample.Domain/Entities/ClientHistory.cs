using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebhookSample.Domain.Enums;

namespace WebhookSample.Domain.Entities
{
    public class ClientHistory : BaseEntity
    {
        [Required]
        public Guid ClientId { get; private set; }
        public Client Client { get; private set; }

        [Required]
        [MaxLength(50)]
        public string EventName { get; private set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; private set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; private set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; private set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; private set; } = "ACTIVE";

        public ClientHistory()
        {
        }

        public ClientHistory(Client client, EventName eventName)
        {
            Client = client;
            EventName = eventName.ToString();
            Name = client.Name;
            BirthDate = client.BirthDate;
            Email = client.Email;
            UpdatedAt = client.UpdatedAt;
            Status = client.Status;
        }

    }
}
