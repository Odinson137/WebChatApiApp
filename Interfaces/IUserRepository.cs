using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<UserDTO>> GetUsers();
        Task<User> GetUser(string id = "", string userName = "");
        //void CreateUser(User user);
    }
}
