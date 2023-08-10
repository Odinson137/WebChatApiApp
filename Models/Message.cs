using System.ComponentModel.DataAnnotations.Schema;

namespace WebChatApp.Models
{
    public class Message
    {
        public int MessageID { get; set; }
        
        [ForeignKey("Chat")]
        public int ChatID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public DateTime SendTime { get; set; }
        public string Text { get; set; }
        //public Chat Сhat { get; set; }
    }
}
