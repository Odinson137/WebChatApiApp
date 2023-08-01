using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
    }
}
