using System.Collections.Concurrent;

namespace WebChatApp.Data
{
    public class UserManager
    {
        private ConcurrentDictionary<int, string> _usersId = new ConcurrentDictionary<int, string>();

        public void AddUserId(int userId, string userConnectionId)
        {
            _usersId.TryAdd(userId, userConnectionId);
        }

        public string GetUserConnectionId(int userId)
        {
            if (_usersId.TryGetValue(userId, out string connectionUserId))
            {
                return connectionUserId;
            }
            throw new InvalidOperationException("User not found");
        }
    }
}
