using stamp_back.Models;

namespace stamp_back.Interfaces
{
    public interface IChatRepository
    {
        ICollection<Chat> GetChats();
        ICollection<Chat> GetAllChatsByUserId(Guid userID);
        Chat GetChatByName(string name);

        Chat GetChatById(Guid id);
       
    }
}
