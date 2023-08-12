using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebChatApp.Data.Hubs
{
    public class ChatHub : Hub
    {
        private UserManager _userManager;

        public ChatHub(UserManager userManager)
        {
            _userManager = userManager;
        }

        public override async Task OnConnectedAsync()
        {
            
            Console.WriteLine("Подключился новый пользователь!");
        }

        public void SendMessage(int userId)
        {
            try
            {
                string userConnectionId = Context.ConnectionId;
                Console.WriteLine(userId + " " + userConnectionId);
                _userManager.AddUserId(userId, userConnectionId);
            } catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
