using Microsoft.AspNetCore.Components.Web;
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

        public bool FindUserId(string userId)
        {
            return _usersId.ContainsKey(userId);
        }

        public string GetConnectionId(string userId)
        {
            return _usersId[userId];
        }

        public string GetUserConnectionId(string userId)
        {
            if (_usersId.TryGetValue(userId, out string connectionUserId))
            {
                return connectionUserId;
            }
            throw new InvalidOperationException("User not found");
        }

        //public bool DeleteUser(string userId = "", string userConnectionId = "")
        //{
        //    if (userId == "")
        //    {
        //        userId = GetIdForValue(userConnectionId);
        //    }
        //    if (_usersId.TryRemove(userId, out string temp))
        //    {
        //        return true;
        //    } 
        //    return false;
        //}

        private string GetIdForValue(string connectionId)
        {
            return _usersId
                .Where(x => x.Value == connectionId)
                .Select(x => x.Key).FirstOrDefault();
        }
    }
}
