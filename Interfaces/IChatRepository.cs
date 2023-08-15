using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IChatRepository
    {
        bool CreateNewChat(Chat chat);
        Chat GetChat(int chatId);
        ICollection<Chat> GetChats();
        bool Save();
        User GetUser(int userID);
        void UpdateState(User user);
        int DeleteChat(int chatId);
        ICollection<ChatDTO> GetUserChats(int userId);
    }
}
