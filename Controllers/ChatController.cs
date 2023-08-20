using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebChatApp.DTO;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ChatController : Controller 
    {

        private readonly IChatRepository _chatRepository;
        private readonly UserManager<User> _userManager;
        public ChatController(IChatRepository chatRepository, UserManager<User> userManager)
        {
            _chatRepository = chatRepository;
            _userManager = userManager;
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chat>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetChats()
        {
            var chats = await _chatRepository.GetChats();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Chat>))]
        public async Task<IActionResult> GetUserChats(string userId)
        {
            ICollection<ChatDTO> chats = await _chatRepository.GetUserChats(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }

        [HttpPost]
        public async Task<IActionResult> CreateChat(string title, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("The user with this id was not found");
            }
            _chatRepository.UpdateState(user);

            Chat chat = new Chat()
            {
                ChatId = 0,
                Title = title,
                Users = new List<User>() { user },
            };

            await _chatRepository.CreateNewChat(chat);

            if (await _chatRepository.Save())
            {
                return Ok(chat.ChatId);
            }
            else
            {
                return BadRequest("Не получилось - не фартануло");
            }
        }

        [HttpPost("{chatId}/{userName}")]
        public async Task<IActionResult> AddUserToChat(int chatId, string userName)
        {
            var chat = await _chatRepository.GetChatWithUsers(chatId);
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            _chatRepository.UpdateState(user);

            chat.Users.Add(user);
            if (await _chatRepository.Save())
            {
                return Ok("The user is successfully connected to the chat");
            }
            return BadRequest("Error when adding a user to a chat");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTitleChat(int chatId, string title)
        {
            Chat chat = await _chatRepository.GetChat(chatId);
            chat.Title = title;

            if (await _chatRepository.Save())
            {
                return Ok("Name changed successfully");
            } else
            {
                return BadRequest("The name couldn't be changed");
            }
        }

        [HttpDelete("{chatId}")]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            if (await _chatRepository.DeleteChat(chatId) == 1)
            {
                return Ok("Chat successfully deleted");
            }
            else
            {
                return BadRequest("The chat has not been deleted");
            }
        }
    }
}
