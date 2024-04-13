using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebhookSample.Domain.Enums;
using WebhookSample.Domain.Requests.Clients;

namespace WebhookSample.Domain.Entities
{
    public class Client : BaseEntity
    {
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

        [JsonIgnore]
        public ICollection<ClientHistory> Histories { get; set; }

        public Client()
        {
        }

        public Client(string name, DateOnly birthDate, string email, bool status, List<ClientHistory> histories)
        {
            Name = name;
            BirthDate = birthDate;
            Email = email;
            Status = status ? "ACTIVE" : "INACTIVE";
            Histories = histories;
        }

        public void AddHistory(Client clientToAddHistory, EventName eventName)
        {
            Histories = new List<ClientHistory>
            {
                new ClientHistory(clientToAddHistory, eventName)
            };
        }

        public void ChangeStatus(Client client, bool status)
        {
            client.Status = status ? "ACTIVE" : "INACTIVE";
            client.UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateInfos(Client client, UpdateClientRequest request)
        {
            client.UpdatedAt = DateTime.UtcNow;
            client.BirthDate = request.BirthDate;
            client.Email = request.Email;
            client.Name = request.Name;
        }
    }
}
