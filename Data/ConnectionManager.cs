using System.Collections.Concurrent;

namespace WebChatApp.Data
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, string> _usersId = new ConcurrentDictionary<string, string>();

        public bool AddUserId(string userId, string userConnectionId)
        {
            if (_usersId.TryAdd(userId, userConnectionId))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> FindUserId(string userId)
        {
            return _usersId.ContainsKey(userId);
        }

        public async Task<string> GetConnectionId(string userId)
        {
            if (_usersId.TryGetValue(userId, out string connectionId))
            {
                return connectionId;
            }
            else
            {
                Console.WriteLine("НЕ найдён");
                return null;
            }
        }

        public async Task<bool> DeleteUserById(string userId)
        {
            if (_usersId.TryRemove(userId, out string? _))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteUserByConnectionIdAsync(string userConnectionId)
        {
            string userId = await GetIdForValue(userConnectionId);
            if (userId != null)
            {
                return await DeleteUserById(userId);
            } 
            return false;
        }

        private async Task<string?> GetIdForValue(string connectionId)
        {
            var userId = _usersId.Where(x => x.Value == connectionId)
                            .Select(x => x.Key).FirstOrDefault();
            return userId;
        }
    }
}
