using Microsoft.AspNetCore.SignalR;

namespace WebChatApp.Data.Hubs
{
    public class ChatHub : Hub
    {
        Dictionary<int, string> userDict = new Dictionary<int, string>();
        public async Task Send(string message)
        {
            Console.WriteLine(message);
            //await this.Clients.All.SendAsync("Receive", username, message);
            string userId = Context.ConnectionId;
            Console.WriteLine(userId);

            // Передаем идентификатор клиенту
            await Clients.Caller.SendAsync("ReceiveUserId", userId);
        }
    }
}
