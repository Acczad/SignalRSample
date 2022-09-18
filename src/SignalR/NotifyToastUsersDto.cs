namespace SignalR
{
    public class NotifyToastUsersDto
    {
        public List<long> UserIds { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationTypeAlias { get; set; }
        
    }
   
}
