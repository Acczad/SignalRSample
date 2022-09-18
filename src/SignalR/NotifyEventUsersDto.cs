namespace SignalR
{
    public class NotifyEventUsersDto
    {
        public List<long> UserIds { get; set; }
        public string EventTypeAlias { get; set; }
        public string Url { get; set; }
    }
   
}
