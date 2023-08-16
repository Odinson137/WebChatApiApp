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
        private readonly UserManager _userManager;

        public MessageController(IHubContext<ChatHub> hubContext, IMessageRepository messageRepository, UserManager userManager)
        {
            _hubContext = hubContext;
            _messageRepository = messageRepository;
            _userManager = userManager;
        }

        [HttpGet("{chatId}")]
        [ProducesResponseType(200, Type = typeof(Message))]
        public IActionResult GetUserMessages([FromRoute] int chatId)
        {
            ICollection<Message> messages = _messageRepository.GetChatMessages(chatId);
            if (messages == null) return BadRequest(ModelState);

            return Ok(messages);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult GetUser([FromBody] Message message)
        {
            message.SendTime = DateTime.Now;
            _messageRepository.AddNewMessage(message);

            // потом отправить эти данные прямикос из клиента, где эта инфа содержится
            ICollection<int> usersId = _messageRepository.GetIdChatUsers(message.ChatID);
            foreach (int userId in usersId)
            {
                if (message.UserID != userId && _userManager.FindUserId(userId))
                    _hubContext.Clients.Client(_userManager.GetConnectionId(userId))
                        .SendAsync("OnReceiveMessage", message.UserID, message.ChatID, message.Text);
            }

            return Ok("Сообщение успешно создано");
        }

        //[HttpPost]
        //public async Task<IActionResult> SendMessage([FromBody] string message)
        //{
        //    await _hubContext.Clients.All.SendAsync("ReceiveMessage", message);
        //    Console.WriteLine(message);
        //    return Ok();
        //}
    }
}
