using stamp_back.Models;

namespace stamp_back.Dto
{
    public class ChatDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Message> Messages { get; set; }

        public ICollection<UserChat> UserChats { get; set; }

        public class ChatCreateDto
        {
            public string Name { get; set; }

            public Guid User1 { get; set; }
            public Guid User2 { get; set; }
        }
    }
}
