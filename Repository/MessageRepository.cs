using WebChatApp.Data;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;

        public MessageRepository(DataContext context)
        {
            _context = context;
        }

        public void AddNewMessage(Message message)
        {
            //message.SendTime = DateTime.Now;
            //Message message = new Message()
            //{
            //    UserID = userId,
            //    ChatID = chatId,
            //    Text = text,
            //    SendTime = DateTime.Now
            //};

            _context.Add(message);
            _context.SaveChanges();
        }

        public ICollection<Message> GetChatMessages(int chatId)
        {
            ICollection<Message> messages = _context.Messages.Where(chat => chat.ChatID == chatId).ToList();
            return messages;
        }
    }
}
