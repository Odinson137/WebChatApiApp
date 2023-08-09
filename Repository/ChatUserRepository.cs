using Microsoft.AspNetCore.Mvc;
using WebChatApp.Data;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Repository
{
    public class ChatUserRepository : IChatUserRepository
    {
        private readonly DataContext _context;

        public ChatUserRepository(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CreateChat>))]
        [ProducesResponseType(400)]
        public ICollection<CreateChat> GetUserChats(int userId)
        {
            ICollection<CreateChat> chats = _context.Users.Where(c => c.UserID == userId)
                                                .SelectMany(x=> x.Chats)
                                                .Select(x=> new CreateChat() 
                                                { 
                                                    ChatID = x.ChatID,  
                                                    Title = x.Title 
                                                })
                                                .ToList();

            return chats;
        }
    }
}
