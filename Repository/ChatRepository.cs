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

        public ICollection<Chat> GetChats()
        {
            var chats = _context.Chats.ToList();
            return chats;
        }

        public bool CreateNewChat(int userID, ChatDTO createChat)
        {
            User? user = _context.Users.Find(userID);
            if (user == null)
            {
                Console.WriteLine("Пользователь с таким id не найден");
                return false;
            }
            Chat chat = new Chat()
            {
                Title = createChat.Title,
                Users = new List<User>() { user },
            };
            _context.Chats.Add(chat);
            _context.SaveChanges();
            return true;
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
