namespace WebChatApp.Models
{
    public class ChatUser
    {
        public string Id { get; set; }
        public User User { get; set; }
        public int ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
