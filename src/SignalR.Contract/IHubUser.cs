namespace SignalR.Contract
{
    public interface IHubUser
    {
        public long UserId { get; }
        public DateTime ConnectDateTime { get; }
        public List<string> ConnectionId { get; }
    }
}