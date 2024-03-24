using System.ComponentModel.DataAnnotations;

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
        public  string Email { get; set; }
    }
}
