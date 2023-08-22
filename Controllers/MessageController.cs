using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebChatApp.Data;
using WebChatApp.Data.Hubs;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class MessageController : Controller
    {
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMessageRepository _messageRepository;
        private readonly ConnectionManager _сonnectionManager;

        public MessageController(IHubContext<ChatHub> hubContext, IMessageRepository messageRepository, ConnectionManager сonnectionManager)
        {
            _hubContext = hubContext;
            _messageRepository = messageRepository;
            _сonnectionManager = сonnectionManager;
        }

        [HttpGet("{chatId}")]
        [ProducesResponseType(200, Type = typeof(Message))]
        public async Task<IActionResult> GetUserMessages([FromRoute] int chatId)
        {
            ICollection<Message> messages = await _messageRepository.GetChatMessages(chatId);
            if (messages == null) return BadRequest(ModelState);

            return Ok(messages);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetUser([FromBody] Message message)
        {
            message.SendTime = DateTime.Now;
            await _messageRepository.AddNewMessage(message);
            await _messageRepository.Save();
            // потом отправить эти данные прямикос из клиента, где эта инфа содержится
            ICollection<string> usersId = await _messageRepository.GetIdChatUsers(message.ChatId);
            foreach (string userId in usersId)
            {
                if (message.Id != userId && await _сonnectionManager.FindUserId(userId))
                {
                    await _hubContext.Clients.Client(await _сonnectionManager.GetConnectionId(userId))
                        .SendAsync("OnReceiveMessage", message.Id, message.ChatId, message.Text);
                }
            }

            return Ok("Сообщение успешно создано");
        }
    }
}
