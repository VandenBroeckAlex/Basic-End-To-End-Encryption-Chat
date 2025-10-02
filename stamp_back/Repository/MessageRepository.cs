using stamp_back.Data;
using stamp_back.Interfaces;
using stamp_back.Models;

namespace stamp_back.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context) => this._context = context;

        public ICollection<Message>? GetAllMessages()
        {
            var Messages = _context.Messages?.OrderBy(m => m.Id).ToList() ;

            return Messages;
        }

        public ICollection<Message>? GetAllChatMessages(Guid chatId) 
        { 
            var chatMessage = _context.Messages?.Where(p=>p.ChatId == chatId).ToList();
            
            ICollection<Message> messages = new List<Message>();

            foreach (var link in chatMessage)
            { 
                var message = _context.Messages.Where(m=>m.Id == link.Id).First();

                if(message != null) messages.Add(message);
            }
            return messages;                         
        }
    }
}
