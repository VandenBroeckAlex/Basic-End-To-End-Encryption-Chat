using stamp_back.Models;

namespace stamp_back.Interfaces
{
    public interface IUserChatRepository
    {
        bool CreateUserChat(UserChat userChat);
        bool Save();
    }
}
