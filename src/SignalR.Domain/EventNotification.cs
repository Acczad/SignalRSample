using SignalR.Contract;

namespace SignalR.Domain
{
    public class EventNotification : IEventNotification, IBaseNotification
    {
        public string EventTypeAlias { get; set; }
        public string Url { get; set; }
    }
}
