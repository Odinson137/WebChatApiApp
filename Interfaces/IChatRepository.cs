using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IChatRepository
    {
        Task<bool> CreateNewChat(Chat chat);
        Task<Chat> GetChat(int chatId);
        Task<ICollection<Chat>> GetChats();
        Task<bool> Save();
        Task<User> GetUser(string userID);
        void UpdateState(User user);
        //int GetChatId(int userId);
        Task<int> DeleteChat(int chatId);
        Task<ICollection<ChatDTO>> GetUserChats(string userId);
    }   
}
