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

        public async Task AddNewMessage(Message message)
        {
            await _context.AddAsync(message);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Message>> GetChatMessages(int chatId)
        {
            var chatWithMessages = await _context.Chats
                                                .Include(chat => chat.Messages)
                                                .Where(chat => chat.ChatId == chatId)
                                                .FirstOrDefaultAsync();

            return chatWithMessages?.Messages ?? new List<Message>();
        }


        public async Task<ICollection<string>> GetIdChatUsers(int chatId)
        {
            var chatUsers = await _context.Chats
                                        .Include(u => u.Users)
                                        .Where(chat => chat.ChatId == chatId)
                                        .FirstOrDefaultAsync();

            return chatUsers?.Users.Select(user => user.Id)
                                .ToList() ?? new List<string>();
        }
    }
}
