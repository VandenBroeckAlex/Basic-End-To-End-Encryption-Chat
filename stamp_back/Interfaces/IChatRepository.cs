using stamp_back.Models;

namespace stamp_back.Interfaces
{
    public interface IChatRepository
    {
        //Read
        ICollection<Chat> GetChats();
        ICollection<Chat> GetAllChatsByUserId(Guid userID);
        Chat GetChatByName(string name);
        Chat GetChatById(Guid id);
        bool ChatExists(Guid id);

        Guid? ChatExistsByUsers(Guid userId1, Guid userId2);
        //Post Methods
        bool CreateChat(Chat chat);
        bool Save();
    }
}
