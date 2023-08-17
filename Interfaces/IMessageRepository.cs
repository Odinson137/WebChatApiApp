using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IMessageRepository
    {
        Task<ICollection<Message>> GetChatMessages(int chatId);
        Task AddNewMessage(Message message);
        Task Save();

        Task<ICollection<string>> GetIdChatUsers(int chatId);
    }
}
