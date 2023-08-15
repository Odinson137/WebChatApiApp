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

        public Chat GetChat(int chatId)
        {
            var chat = _context.Chats.Find(chatId);
            _context.Entry(chat).State = EntityState.Unchanged;
            return chat;
        }

        public ICollection<Chat> GetChats()
        {
            var chats = _context.Chats.ToList();
            return chats;
        }

        public User GetUser(int userID)
        {
            return _context.Users.Find(userID);
        }

        public void UpdateState(User user)
        {
            _context.Entry(user).State = EntityState.Unchanged;
        }

        public bool CreateNewChat(Chat chat)
        {
            _context.Chats.Add(chat);
            return true;
        }

        public int DeleteChat(int chatId)
        {
            return _context.Chats.Where(chat => chat.ChatID == chatId).ExecuteDelete();
        }

        public bool Save()
        {
            if (_context.SaveChanges() != 0)
            {
                return true;
            }
            return false;
        }

        public ICollection<ChatDTO> GetUserChats(int userId)
        {
            ICollection<ChatDTO> chats = _context.Users
                .Include(u => u.Chats)
                .ThenInclude(c => c.Users)
                .Where(u => u.UserID == userId)
                .SelectMany(u => u.Chats)
                .Select(x => new ChatDTO()
                {
                    ChatID = x.ChatID,
                    Title = x.Title,
                    Users = x.Users.Select(u => new UserDTO()
                    {
                        UserID = u.UserID,
                        Name = u.Name,
                        LastName = u.LastName,
                    }).ToList()
                })
                .ToList();


            return chats;
            //User user = _context.Users.Find(userId);
            //_context.Entry(user).Collection(c );
            //ICollection<Chat> chats = _context.Chats.Include(x => x.Users).Where(c => c.Users.Any(u => u.UserID == userId))
            //                            .Select(c => new Chat()
            //                            {
            //                                c.ChatID

            //                            }).ToList();
            //ICollection<Chat> chats = _context.Users.Find(userId).Chats;
            //ICollection<Chat> chats = _context.Users.Include(x => x.Chats).Where(x => x.UserID == userId).FirstOrDefault()
            //    .Chats.SelectMany(x => new Chat()
            //    {
            //        x.ChatID,
            //        x.Title,
            //        x.Users
            //    });


            //var chats = _context.Chats
            //    .Include(chat => chat.Users)
            //    .Where(chat => chat.Users.Any(user => user.UserID == userId));
            //var chats = _context.Users
            //     .Include(chat => chat.Chats).Where(u => u.UserID == userId).FirstOrDefault().Chats;
            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    // Другие настройки...
            //};

            //string jsonString = JsonSerializer.Serialize(chats, options);

            //return jsonString;
        }
    }
}
