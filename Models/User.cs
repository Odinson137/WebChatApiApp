using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebChatApp.Models
{
    public class User : IdentityUser
    {
        //[Required]
        //public string UserName { get; set; }
        //public string Name { get; set; }
        //public string LastName { get; set; }
        //[Required]
        //[PasswordPropertyText]
        //[JsonIgnore]
        //public string Password { get; set; }
        public ICollection<Chat> Chats { get; set; }  
    }
}
