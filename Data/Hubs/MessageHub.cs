using Microsoft.AspNetCore.SignalR;

namespace WebChatApp.Data.Hubs
{
    public class ChatHub : Hub
    {
        public async Task Send(string username, string message)
        {
            Console.WriteLine(message);
            await this.Clients.All.SendAsync("Receive", username, message);
        }
    }
}
