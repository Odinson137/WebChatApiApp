using Microsoft.EntityFrameworkCore;
using WebChatApp.Data;
using WebChatApp.DTO;
using WebChatApp.Interfaces;
using WebChatApp.Models;

namespace WebChatApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ICollection<UserDTO>> GetUsers()
        {
            ICollection<UserDTO> users = await _context.Users.Select(x => new UserDTO()
            {
                Id = x.Id,
                UserName = x.UserName,
            }).ToListAsync();

            return users;
        }

        public async Task<bool> CheckUser(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task<User> GetUserByUserNameAsync(string userName)
        {
            return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> DeleteUserMessages(string userId)
        {
            return await _context.Messages.Where(message => message.Id == userId).ExecuteDeleteAsync();
        }
        public async Task<int> DeleteEmptyChats()
        {
            return await _context.Chats.Where(chat => chat.Users.Count() == 0).ExecuteDeleteAsync();
        }

        public async Task<int> DeleteUser(string userName)
        {
            return await _context.Users.Where(user => user.UserName == userName).ExecuteDeleteAsync();
        }

    }
}
