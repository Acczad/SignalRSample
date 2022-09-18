using Microsoft.AspNetCore.SignalR;
using SignalR;
using SignalR.Contract;

namespace ICD.SignalR.Api.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IHubNotifictionContext hubNotifictionContext;

        public NotificationHub(IHubNotifictionContext hubNotifictionContext)
        {
            this.hubNotifictionContext = hubNotifictionContext;
        }

        //End User EndPoints
        public Task<HubMethodResultDto> Login(long UserId)
        {
            var connectionId = Context.ConnectionId;
            return Task.FromResult(hubNotifictionContext.Login(UserId, connectionId));
        }


        // Manage Users
        private void AddUser(string connectionId)
        {
            hubNotifictionContext.AddUser(connectionId);
        }
        private void RemoveUser(string connectionId)
        {
            hubNotifictionContext.RemoveUser(connectionId);
        }


        public override async Task OnConnectedAsync()
        {
            var connectionId = Context.ConnectionId;
            AddUser(connectionId); //unknow user connect to hub.
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var connectionId = Context.ConnectionId;
            RemoveUser(connectionId);
            await base.OnDisconnectedAsync(exception);
        }

    }
}
