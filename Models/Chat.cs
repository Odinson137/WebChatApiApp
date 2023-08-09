using System.ComponentModel.DataAnnotations.Schema;

namespace WebChatApp.Models
{
    public class Chat
    {
        public int ChatID { get; set; }
        public string Title { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Message> Messages { get; set; }
    }

    public class CreateChat
    {
        public int ChatID { get; set; }
        public string Title { get; set; }
    }
}
