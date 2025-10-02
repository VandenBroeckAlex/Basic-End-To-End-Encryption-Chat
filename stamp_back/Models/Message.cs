using System.ComponentModel.DataAnnotations;

namespace stamp_back.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }        
        public string Body { get; set; }

        // Foreign key
        public Guid UserId { get; set; }
        public Guid ChatId { get; set; }

        // Navigation property
        public User User { get; set; }
        public Chat Chat { get; set; }

    }
}
