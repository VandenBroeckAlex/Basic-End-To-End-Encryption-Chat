using stamp_back.Data;
using stamp_back.Models;

namespace stamp_back.Interfaces
{
    public interface IMessageRepository
    {
        ICollection<Message> GetAllMessages();

        ICollection<Message> GetAllChatMessages(Guid chatId);

        Message PostMessage(Message _message);
    }
}