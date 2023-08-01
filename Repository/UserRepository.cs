using Microsoft.AspNetCore.Http.HttpResults;
using WebChatApp.Data;
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

        public ICollection<User> GetUsers()
        {
            ICollection<User> users = _context.Users.ToList();
            return users;
        }

        public void NewUser(User user)
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
