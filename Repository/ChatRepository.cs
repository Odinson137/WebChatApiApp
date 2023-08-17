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
            _context.Entry(chat).State = EntityState.Unchanged;
            return chat;
        }

        //public int GetChatId(int userId)
        //{
        //    int chatId = _context.Users.Include(user => user.Chats)
        //                    .Where(user => user.UserID == userId)
        //                    .Select(x => x.Chats.Last())
        //                    .FirstOrDefault()?.ChatID ?? 0;
        //    return chatId;
        //}

        public async Task<ICollection<Chat>> GetChats()
        {
            var chats = await _context.Chats.ToListAsync();
            return chats;
        }

        public async Task<User> GetUser(string userID)
        {
            return await _context.Users.FindAsync(userID);
        }

        public void UpdateState(User user)
        {
            _context.Entry(user).State = EntityState.Unchanged;
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
                .ThenInclude(c => c.Users)
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
