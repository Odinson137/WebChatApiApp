using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IChatRepository
    {
        bool CreateNewChat(int UserID, ChatDTO createChat);
        ICollection<Chat> GetChats();
        ICollection<ChatDTO> GetUserChats(int userId);
    }
}
