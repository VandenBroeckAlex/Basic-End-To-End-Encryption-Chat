using System.ComponentModel.DataAnnotations;

namespace stamp_back.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public string TimeStamp { get; set; }
        public Guid SenderId { get; set; }
        public string Body { get; set; }

    }
}
