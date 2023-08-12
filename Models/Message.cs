using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChatApp.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        
        [ForeignKey("Chat")]
        public int ChatID { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public DateTime SendTime { get; set; }
        public string Text { get; set; }
    }
}
