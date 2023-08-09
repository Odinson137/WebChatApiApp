using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IChatUserRepository
    {
        ICollection<CreateChat> GetUserChats(int userId);
    }
}
