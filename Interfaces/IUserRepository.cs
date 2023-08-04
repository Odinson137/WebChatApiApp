using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<UserCreate> GetUsers();
        User GetUser(int id);
        User GetUser(string name);
        void CreateUser(User user);
    }
}
