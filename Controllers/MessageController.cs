using Microsoft.AspNetCore.Mvc;
using WebChatApp.Interfaces;

namespace WebChatApp.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageRepository _messageRepository;

        public MessageController(IMessageRepository userRepository)
        {
            _messageRepository = userRepository;
        }
    }
}
