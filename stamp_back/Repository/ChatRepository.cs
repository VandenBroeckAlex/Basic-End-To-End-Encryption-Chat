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


        public bool ChatExists(Guid id)
        {
            return _context.Chats.Any(c=> c.Id == id);
        }

        public Guid? ChatExistsByUsers(Guid userId1, Guid userId2)
        {
            var chatIdsUser1 = _context.UserChats
                .Where(uc => uc.UserId == userId1)
                .Select(uc => uc.ChatId);

            var chatExists = _context.UserChats
                .FirstOrDefault(uc => uc.UserId == userId2 && chatIdsUser1.Contains(uc.ChatId));

            return chatExists?.ChatId;
        }

        public bool CreateChat(Chat chat)
        {
            _context.Add(chat);

            return Save();
        }

        public bool Save()
        {
          var saved = _context.SaveChanges();
          return saved > 0 ? true : false;
        }
        //public User GetUserByEmail(string email
        //{
        //    return _context.Users.Where(p => p.Email == email).FirstOrDefault();
        //}

    }
}
