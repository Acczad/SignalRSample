using Microsoft.AspNetCore.SignalR;
using SignalR;
using SignalR.Contract;
using SignalR.Domain;
using System.Collections.Concurrent;
using System.Text.Json;

namespace ICD.SignalR.Api.Hubs
{
    public class HubNotifictionContext : IHubNotifictionContext
    {
        private readonly IHubContext<NotificationHub> _notifHubContext;
        public ConcurrentDictionary<long, HubUser> HubUsers { get; }

        public HubNotifictionContext(IHubContext<NotificationHub> notifHubContext)
        {
            HubUsers = new ConcurrentDictionary<long, HubUser>();
            _notifHubContext = notifHubContext;
        }
        public IEnumerable<IHubUser> GetAllHubUsers()
        {
            List<HubUser> users = HubUsers.Values.ToList();
            return users;
        }
        public IHubUser GetUserByConnectionId(string connectionId)
        {
            var result = HubUsers.FirstOrDefault(q => q.Value.ConnectionId.Any(q => q == connectionId));
            if (result.Value == null) return null;
            return result.Value;
        }
        public List<string> GetConnectionIdByUserId(long UserId)
        {
            HubUser user;
            HubUsers.TryGetValue(UserId, out user);
            if (user == null) return null;
            return user.ConnectionId.ToList();
        }
        public async Task NotifyEventToAllUsersAsync(IEventNotification notificationObject)
        {
            var data = JsonSerializer.Serialize(notificationObject);
            await _notifHubContext.Clients.All.SendAsync("notifyevent", data);
        }
        public async Task NotifyToastToAllUsersAsync(IToastNotification notificationObject)
        {
            var data = JsonSerializer.Serialize(notificationObject);
            await _notifHubContext.Clients.All.SendAsync("notifytoast", data);
        }
        public async Task NotifyEventToUserAsync(long UserId, IEventNotification notificationObject)
        {
            var connectionId = GetConnectionIdByUserId(UserId);
            if (connectionId == null || connectionId.Any() == false)
            {
                // we can store message and send it later.
                return;
            }
            foreach (var item in connectionId)
            {
                var data = JsonSerializer.Serialize(notificationObject);
                await _notifHubContext.Clients.Client(item).SendAsync("notifyevent", data);
            }
        }
        public async Task NotifyToastToUserAsync(long UserId, IToastNotification notificationObject)
        {
            var connectionId = GetConnectionIdByUserId(UserId);
            if (connectionId == null || connectionId.Any() == false)
            {
                // we can store message and send it later.
                return;
            }
            foreach (var item in connectionId)
            {
                var data = JsonSerializer.Serialize(notificationObject);
                await _notifHubContext.Clients.Client(item).SendAsync("notifytoast", data);
            }

        }
        public async Task NotifyToastToUsersAsync(List<long> UserIds, IToastNotification notificationObject)
        {
            foreach (var UserId in UserIds)
            {
                await NotifyToastToUserAsync(UserId, notificationObject);
            }
        }
        public async Task NotifyEventToUsersAsync(List<long> UserIds, IEventNotification notificationObject)
        {
            foreach (var UserId in UserIds)
            {
                await NotifyEventToUserAsync(UserId, notificationObject);
            }
        }

        // we can send token in request header
        // it's just for server action demo.
        public HubMethodResultDto Login(long UserId, string connectionId) 
        {
            // TODO add maximum user sessions.
            var result = new HubMethodResultDto();
            try
            {
                LoginUser(UserId, connectionId);
                result.Success = true;
                return result;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return result;
            }
        }
        
        private void LoginUser(long UserId, string connectionId)
        {
            if (HubUsers.TryGetValue(UserId, out HubUser hubUser) == false)
            {
                HubUsers[UserId] = new HubUser(UserId, connectionId);
            }
            else
            {
                if (HubUsers[UserId].ConnectionId.Any(q => q == connectionId) == false)
                    HubUsers[UserId].ConnectionId.Add(connectionId);
            }
        }
        public void AddUser(string connectionId)
        {
            // you can allow add user without login.
            // HubUsers[connectionId] = new HubUser(-1, connectionId, 1); //unknow user connect to hub.
        }
        public void RemoveUser(string connectionId)
        {
            var person = GetUserByConnectionId(connectionId);
            if (person == null) { return; }

            var UserId = person.UserId;

            if (person.ConnectionId.Any(q => q == connectionId) && person.ConnectionId.Count <= 1)
            {
                HubUsers.TryRemove(UserId, out HubUser hubUser);
                return;
            }

            // TODO lock before delete
            person.ConnectionId.Remove(connectionId);
        }
    }
}