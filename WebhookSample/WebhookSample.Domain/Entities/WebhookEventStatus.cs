using System.ComponentModel.DataAnnotations;
using WebhookSample.Domain.Enums;

namespace WebhookSample.Domain.Entities
{
    public class WebhookEventStatus
    {
        [Key]
        public Guid Id { get; private set; }

        [Required]
        [MaxLength(50)]
        public string EventName { get; private set; }

        [Required]
        [MaxLength(20)]
        public string Context { get; private set; }

        [Required]
        public string Message { get; private set; }

        [Required]
        public DateTime SentDate { get; private set; }

        [Required]
        [MaxLength(10)]
        public string Status { get; private set; }

        public WebhookEventStatus()
        {
            
        }

        public WebhookEventStatus(string eventName, string context, string message, bool success = true)
        {
            Id = Guid.NewGuid();
            EventName = eventName;
            Context = context;
            Message = message;
            SentDate = DateTime.UtcNow;
            Status = success ? "SUCCESS" : "FAILED";
        }
    }
}
