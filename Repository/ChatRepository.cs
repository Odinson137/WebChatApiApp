using WebChatApp.Data;
using WebChatApp.Interfaces;
using WebChatApp.Models;

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

        public bool CreateNewChat(int UserID, CreateChat createChat)
        {
            User user = _context.Users.Where(u => u.UserID == UserID).FirstOrDefault();
            User test = new User { Name = "Who", LastName = "asd", Password = "ase42q33232" };
            _context.Users.Add(test);
            Chat chat = new Chat()
            {
                Title = createChat.Title,
                Users = new List<User>() { user, test },
            };
            _context.Chats.Add(chat);
            _context.SaveChanges();
            return true;
        }
    }
}
