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

        //[HttpGet("{userId}")]
        //[ProducesResponseType(200, Type = typeof(int))]
        //[ProducesResponseType(400)]
        //public IActionResult GetChatId(int userId)
        //{
        //    int chatId = _chatRepository.GetChatId(userId);

        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    if (chatId == 0)
        //    {
        //        return BadRequest("Не найден");
        //    }


        //    return Ok(chatId);
        //}

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateChat(string title, int userID)
        {
            User user = _chatRepository.GetUser(userID);
            if (user == null)
            {
                return BadRequest("Пользователь с таким id не найден");
            }
            _chatRepository.UpdateState(user);

            Chat chat = new Chat()
            {
                ChatID = 0,
                Title = title,
                Users = new List<User>() { user },
            };

            _chatRepository.CreateNewChat(chat);

            if (_chatRepository.Save())
            {
                return Ok(chat.ChatID);
            }
            else
            {
                return BadRequest("Не получилось - не фартануло");
            }
        }

        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult UpdateTitleChat(int chatId, string title)
        {
            Chat chat = _chatRepository.GetChat(chatId);
            chat.Title = title;

            if (_chatRepository.Save())
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
        public IActionResult DeleteChat(int chatId)
        {
            if (_chatRepository.DeleteChat(chatId) == 1)
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
