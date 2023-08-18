using WebChatApp.DTO;
using WebChatApp.Models;

namespace WebChatApp.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<UserDTO>> GetUsers();
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserByIdAsync(string id);
        //void CreateUser(User user);
    }
}
