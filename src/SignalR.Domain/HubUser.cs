using SignalR.Contract;

namespace SignalR.Domain
{
    public class HubUser : IHubUser
    {
        public HubUser(long userId, string connectionId)
        {
            UserId = userId;
            ConnectDateTime = DateTime.Now;
            if (ConnectionId == null) ConnectionId = new List<string>();

            ConnectionId.Add(connectionId);
        }
        public long UserId { get; }
        public DateTime ConnectDateTime { get; }
        public List<string> ConnectionId { get; set; }
    }
}