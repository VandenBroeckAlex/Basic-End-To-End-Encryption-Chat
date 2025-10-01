using stamp_back.Data;
using stamp_back.Interfaces;
using stamp_back.Models;

namespace stamp_back.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly DataContext _context;

        public ChatRepository(DataContext context) => this._context = context;

        public ICollection<Chat> GetChats()
        { 
            return _context.Chats.OrderBy(x => x.Id).ToList();
        }

        public ICollection<Chat> GetAllChatsByUserId(Guid userID)
        {
            // get all chat id in  user-chat
            var userChats = _context.UserChats.Where(p => p.UserId == userID).ToList();

            var chats = new List<Chat>();
            foreach (var link in userChats) {

                var chat = _context.Chats.Where(p => p.Id == link.ChatId).FirstOrDefault();

                if (chat != null) 
                {
                    chats.Add(chat);
                }
            }

            return chats;
        }

        public Chat GetChatByName(string name) 
        {
            return _context.Chats.Where(p => p.Name == name).FirstOrDefault();
        }

        public Chat GetChatById(Guid id) 
        {
            return _context.Chats.Where(p => p.Id == id).FirstOrDefault();
        }


        //public User GetUserByEmail(string email)
        //{
        //    return _context.Users.Where(p => p.Email == email).FirstOrDefault();
        //}

    }
}
