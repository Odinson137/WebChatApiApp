using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using WebChatApp.Data;
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

        public ChatController(IChatRepository chatRepository)
        {
            _chatRepository = chatRepository;
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
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUserChats(string userId)
        {
            ICollection<ChatDTO> chats = await _chatRepository.GetUserChats(userId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(chats);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateChat(string title, string userId)
        {
            User user = await _chatRepository.GetUser(userId);
            if (user == null)
            {
                return BadRequest("Пользователь с таким id не найден");
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

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateTitleChat(int chatId, string title)
        {
            Chat chat = await _chatRepository.GetChat(chatId);
            chat.Title = title;

            if (await _chatRepository.Save())
            {
                return Ok("Название успешно изменено");
            } else
            {
                return BadRequest("Название не получилось поменять");
            }
        }

        [HttpDelete("{chatId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteChat(int chatId)
        {
            if (await _chatRepository.DeleteChat(chatId) == 1)
            {
                return Ok("Чат успешно удалён");
            }
            else
            {
                return BadRequest("Чат не удалён");
            }
        }
    }
}
