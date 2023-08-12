using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebChatApp.DTO;

namespace WebChatApp.Models
{
    public class Chat
    {
        [Key]
        public int ChatID { get; set; }
        public string Title { get; set; }

        //[JsonIgnore]
        public ICollection<User> Users { get; set; }
        [JsonIgnore]
        public ICollection<Message> Messages { get; set; }
    }
}
