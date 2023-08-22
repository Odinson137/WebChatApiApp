using Microsoft.EntityFrameworkCore;
using WebChatApp.Data;
using WebChatApp.Interfaces;
using WebChatApp.Models;
using WebChatApp.DTO;

namespace WebChatApp.Repository
{
    public class ChatRepository : IChatRepository
    {

        private readonly DataContext _context;

        public ChatRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Chat> GetChat(int chatId)
        {
            var chat = await _context.Chats.FindAsync(chatId);
            return chat;
        }

        public async Task<Chat> GetChatWithUsers(int chatId)
        {
            var chat = await _context.Chats
                .Include(chat => chat.Users)
                .Where(chat => chat.ChatId == chatId)
                .FirstAsync();
            return chat;
        }

        public async Task<ICollection<Chat>> GetChats()
        {
            var chats = await _context.Chats.AsNoTracking().ToListAsync();
            return chats;
        }

        public async Task<User> GetUser(string userID)
        {
            return await _context.Users.FindAsync(userID);
        }

        public async Task<bool> CreateNewChat(Chat chat)
        {
            await _context.Chats.AddAsync(chat);
            return true;
        }

        public async Task<int> DeleteChat(int chatId)
        {
            return await _context.Chats.Where(chat => chat.ChatId == chatId).ExecuteDeleteAsync();
        }

        public async Task<int> DeleteUserMessagesInChat(int chatId)
        {
            return await _context.Messages.Where(m => m.ChatId == chatId).ExecuteDeleteAsync();
        }

        public async Task<bool> Save()
        {
            if (await _context.SaveChangesAsync() != 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ICollection<ChatDTO>> GetUserChats(string userId)
        {
            ICollection<ChatDTO> chats = await _context.Users
                .Include(u => u.Chats)
                .AsNoTracking()
                .Where(u => u.Id == userId)
                .SelectMany(u => u.Chats)
                .Select(x => new ChatDTO()
                {
                    ChatId = x.ChatId,
                    Title = x.Title,
                    Users = x.Users.Select(u => new UserDTO()
                    {
                        Id = u.Id,
                        UserName = u.UserName,
  
                    }).ToList()
                })
                .ToListAsync();

            return chats;
        }
    }
}
