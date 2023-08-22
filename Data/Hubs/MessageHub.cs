using Microsoft.AspNetCore.SignalR;

namespace WebChatApp.Data.Hubs
{
    public class ChatHub : Hub
    {
        private ConnectionManager _connectionManager;

        public ChatHub(ConnectionManager connectionManager)
        {
            _connectionManager = connectionManager;
        }

        public override async Task OnConnectedAsync()
        {
            
            Console.WriteLine("Подключился новый пользователь!");
        }

        //public override async Task OnDisconnectedAsync(Exception exception)
        //{
        //    string userConnectionId = Context.ConnectionId;
        //    _connectionManager.DeleteUserByConnectionId(userConnectionId);
        //    await base.OnDisconnectedAsync(exception);
        //}

        public void SendMessage(string userId)
        {
            try
            {
                string userConnectionId = Context.ConnectionId;
                Console.WriteLine(userId + " " + userConnectionId);
                _connectionManager.AddUserId(userId, userConnectionId);
            } catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
        }
    }
}
