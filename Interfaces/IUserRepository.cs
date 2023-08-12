using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IUserRepository
    {
        ICollection<UserDTO> GetUsers();
        User GetUser(int id);
        User GetUser(string name);
        void CreateUser(User user);
    }
}
