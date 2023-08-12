namespace WebChatApp.DTO
{
    public class ChatDTO
    {
        public int ChatID { get; set; }
        public string Title { get; set; }
        public ICollection<UserDTO> Users { get; set; }
    }
}
