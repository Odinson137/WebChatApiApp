using Microsoft.AspNetCore.SignalR;

namespace WebChatApp.Data.Hubs
{
    public class ChatHub : Hub
    {
        //Dictionary<int, string> usersDict = new Dictionary<int, string>();
        //public async Task Send(int userId)
        //{
            
        //    //await this.Clients.All.SendAsync("Receive", username, message);
        //    string userConnectionId = Context.ConnectionId;
        //    Console.WriteLine(userId);

        //    // Передаем идентификатор клиенту
        //    await Clients.Caller.SendAsync("ReceiveUserId", "Ok");
        //}
    }
}
