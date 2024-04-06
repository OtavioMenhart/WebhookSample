using System.ComponentModel.DataAnnotations;

namespace WebhookSample.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; protected set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
