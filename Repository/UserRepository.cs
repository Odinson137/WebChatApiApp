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

        //public Task CreateUser(User user)
        //{
        //    await _context.Users.Add(user);
        //    await _context.SaveChanges();
        //}

        //public 

        public async Task<bool> CheckUser(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName);
        }

        public async Task<User> GetUser(string id = "", string userName = "")
        {
            if (id == "")
            {
                return await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            } else
            {
                return await _context.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
            }

        }
    }
}
