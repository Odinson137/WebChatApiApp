using Microsoft.EntityFrameworkCore;
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
            _context.Add(message);
            _context.SaveChanges();
        }

        public ICollection<Message> GetChatMessages(int chatId)
        {
            ICollection<Message> messages = _context.Chats
                                            .Include(x => x.Messages)
                                            .Where(chat => chat.ChatID == chatId)
                                            .SingleOrDefault()?
                                            .Messages.ToList() ?? new List<Message>();
            return messages;
        }

        public ICollection<int> GetIdChatUsers(int chatId)
        {
            ICollection<int> chatUsers = _context.Chats
                                            .Include(u => u.Users)
                                            .Where(chat => chat.ChatID == chatId)
                                            .SingleOrDefault()?
                                            .Users
                                            .Select(user => user.UserID)
                                            .ToList() ?? new List<int>();
            return chatUsers;
        }
    }
}
