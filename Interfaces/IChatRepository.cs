using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IChatRepository
    {
        bool CreateNewChat(string title, int UserID);
        ICollection<Chat> GetChats();
        ICollection<ChatDTO> GetUserChats(int userId);
    }
}
