using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebChatApp.Data;
using WebChatApp.DTO;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Controllers
{
    [Route("api/[controller]")]
    public class ChatController : Controller 
    {

        private readonly IChatRepository _chatRepository;

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chat>))]
        [ProducesResponseType(400)]
        public IActionResult GetChats()
        {
            var chats = _chatRepository.GetChats();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            return Ok(chats);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chat>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserChats(int userId)
        {
            ICollection<ChatDTO> chats = _chatRepository.GetUserChats(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateChat(string title, int userID)
        {
            if (_chatRepository.CreateNewChat(title, userID))
            {
                return Ok("Чат успешно создан");
            }
            else
            {
                return BadRequest("Не получилось - не фартануло");
            }
        }
    }
}
