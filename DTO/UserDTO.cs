namespace WebChatApp.DTO
{
    public class UserDTO
    {
        public int UserID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }

    public class UserCreate
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
