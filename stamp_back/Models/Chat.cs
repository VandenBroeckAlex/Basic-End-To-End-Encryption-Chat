namespace stamp_back.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }

        public ICollection<UserChat> UserChats { get; set; }

    }
}
