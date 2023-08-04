using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChatApp.Models
{
    public class User
    {
        public int UserID { get; set; }
        [Required]
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        [PasswordPropertyText]
        public string Password { get; set; }
        public ICollection<Chat> Chats { get; set; }
    }

    public class UserCreate
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
