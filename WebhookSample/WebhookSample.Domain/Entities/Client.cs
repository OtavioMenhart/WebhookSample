using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebhookSample.Domain.Enums;

namespace WebhookSample.Domain.Entities
{
    public class Client : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly BirthDate { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }

        [JsonIgnore]
        public ICollection<ClientHistory> Histories { get; set; }

        public void AddHistory(Client clientToAddHistory, EventName eventName)
        {
            Histories = new List<ClientHistory>
            {
                new ClientHistory(clientToAddHistory, eventName)
            };
        }
    }
}
