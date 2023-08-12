using System.Collections.Concurrent;

namespace WebChatApp.Data
{
    public class UserManager
    {


        private ConcurrentDictionary<int, string> _usersId = new ConcurrentDictionary<int, string>();

        public bool AddUserId(int userId, string userConnectionId)
        {
            if (_usersId.TryAdd(userId, userConnectionId))
            {
                return true;
            }
            return false;
        }

        public bool FindUserId(int userId)
        {
            return _usersId.ContainsKey(userId);
        }

        public string GetConnectionId(int userId)
        {
            return _usersId[userId];
        }

        public string GetUserConnectionId(int userId)
        {
            if (_usersId.TryGetValue(userId, out string connectionUserId))
            {
                return connectionUserId;
            }
            throw new InvalidOperationException("User not found");
        }

        public bool DeleteUser(int userId)
        {
            if (_usersId.TryRemove(userId, out string temp))
            {
                return true;
            } 
            return false;
        }

        public bool DeleteUser(string userConnectionId)
        {
            int userId = GetIdForValue(userConnectionId);
            if (_usersId.TryRemove(userId, out string temp))
            {
                return true;
            }
            return false;
        }

        private int GetIdForValue(string connectionId)
        {
            int userId = _usersId.Where(x => x.Value == connectionId).Select(x => x.Key).FirstOrDefault();
            return userId;
        }
    }
}
