using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IMessageRepository
    {
        ICollection<Message> GetChatMessages(int chatId);

        void AddNewMessage(Message message);
    }
}
