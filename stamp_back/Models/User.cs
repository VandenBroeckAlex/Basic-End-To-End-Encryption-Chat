using System.Text.Json.Serialization;

namespace stamp_back.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        [JsonIgnore] public string Password { get; set; }

        //public string icon_url { get; set; }
        public string? RefreshToken {get; set;}
        public DateTime? RefreshTokenExpiryTime { get; set; }
        // Navigation property to messages
        //public ICollection<Message> Messages { get; set; }

        //foreignkey
        public ICollection<UserChat> UserChats { get; set; }
        
    }
}
