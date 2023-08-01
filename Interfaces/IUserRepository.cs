using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<User> GetUsers();
        User GetUser(int id);
        User GetUser(string name);
        void NewUser(User user);
    }
}
