using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IChatRepository
    {
        bool CreateNewChat(int UserID, CreateChat createChat);
        ICollection<Chat> GetChats();
    }
}
