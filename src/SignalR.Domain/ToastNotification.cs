using SignalR.Contract;

namespace SignalR.Domain
{
    public class ToastNotification: IToastNotification, IBaseNotification
    {
        public string Url { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string NotificationTypeAlias { get; set; }
    }
}
