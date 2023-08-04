using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IMessageRepository
    {
        ICollection<Message> GetMessages();
        Message GetMessage(int id);
        bool DeleteMessage(int id);
        ICollection<Message> GetMessages(string userID);
        bool CreateMessage(User user);
    }
}
