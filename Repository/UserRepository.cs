using Microsoft.AspNetCore.Http.HttpResults;
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

        public ICollection<UserDTO> GetUsers()
        {
            ICollection<UserDTO> users = _context.Users.Select(x => new UserDTO()
            {
                UserID = x.UserID,
                Name = x.Name,
                LastName = x.LastName,
            }).ToList();
            return users;
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.UserID == id).FirstOrDefault();
        }

        public User GetUser(string name)
        {
            return _context.Users.Where(u => u.Name == name).FirstOrDefault();
        }
    }
}
