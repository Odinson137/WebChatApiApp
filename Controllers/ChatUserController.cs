using Microsoft.AspNetCore.Mvc;
using WebChatApp.Interfaces;
using WebChatApp.Models;
using WebChatApp.Repository;

namespace WebChatApp.Controllers
{
    [Route("api/[controller]")]
    public class ChatUserController : Controller
    {
        private readonly IChatUserRepository _userChatRepository;

        public ChatUserController(IChatUserRepository userChatRepository)
        {
            _userChatRepository = userChatRepository;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CreateChat>))]
        [ProducesResponseType(400)]
        public IActionResult GetUserChats(int userId)
        {
            var chats = _userChatRepository.GetUserChats(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }
    }


}
