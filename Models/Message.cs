using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebChatApp.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        
        [ForeignKey("Chat")]
        public int ChatId { get; set; }
        [ForeignKey("User")]
        public string Id { get; set; }
        public DateTime SendTime { get; set; }
        public string Text { get; set; }
    }
}
