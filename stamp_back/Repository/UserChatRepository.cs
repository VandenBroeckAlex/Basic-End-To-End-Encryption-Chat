using stamp_back.Data;
using stamp_back.Interfaces;
using stamp_back.Models;

namespace stamp_back.Repository
{
    public class UserChatRepository : IUserChatRepository
    {
        private readonly DataContext _context;
        public UserChatRepository(DataContext context) => this._context = context;

        public bool CreateUserChat(UserChat userChat)
        {
            _context.Add(userChat);

            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
