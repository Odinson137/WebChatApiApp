using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebChatApp.Data;
using WebChatApp.Data.Hubs;
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
        private readonly ConnectionManager _сonnectionManager;
        private readonly IHubContext<ChatHub> _hubContext;

        public ChatController(IChatRepository chatRepository, UserManager<User> userManager, 
            ConnectionManager сonnectionManager, IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _chatRepository = chatRepository;
            _userManager = userManager;
            _сonnectionManager = сonnectionManager;
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
            //_chatRepository.UpdateState(user);

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

            chat.Users.Add(user);
            if (await _chatRepository.Save())
            {
                var connectionId = await _сonnectionManager.GetConnectionId(user.Id);
                await _hubContext.Clients.Client(connectionId)
                    .SendAsync("OnReceiveInvitation", chat);
                return Ok("The user is successfully connected to the chat");
            }
            return BadRequest("Error when adding a user to a chat");
        }

        [HttpPut("{chatId}/{userName}")]
        public async Task<IActionResult> ExitUserFromChat(int chatId, string userName)
        {
            Chat chat = await _chatRepository.GetChatWithUsers(chatId);
            User user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            chat.Users.Remove(user);

            await _chatRepository.DeleteUserMessagesInChat(chatId);

            if (chat.Users.Count == 0)
            {
                if (await _chatRepository.DeleteChat(chatId) == 1)
                {
                    return Ok("The chat is successfully deleted");
                }
                return BadRequest("Error when deleting a user from a chat");
            } else
            {
                if (await _chatRepository.Save())
                {
                    //var connectionId = _сonnectionManager.GetConnectionId(user.Id);
                    //await _hubContext.Clients.Client(connectionId)
                    //    .SendAsync("OnReceiveInvitation", chat);
                    return Ok("The user is successfully deleted from the chat");
                }
                return BadRequest("Error when deleting a user from a chat");
            }

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
