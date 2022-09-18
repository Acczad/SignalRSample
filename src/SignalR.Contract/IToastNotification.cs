namespace SignalR.Contract
{
    public interface IToastNotification
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationTypeAlias { get; set; }
    }
}
