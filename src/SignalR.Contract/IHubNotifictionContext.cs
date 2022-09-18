namespace SignalR.Contract
{
    public interface IHubNotifictionContext
    {
        HubMethodResultDto Login(long UserId, string connectionId);
        void AddUser(string connectionId);
        void RemoveUser(string connectionId);
        IHubUser GetUserByConnectionId(string connectionId);
        IEnumerable<IHubUser> GetAllHubUsers();
        Task NotifyToastToUserAsync(long UserId, IToastNotification notificationObject);
        Task NotifyToastToUsersAsync(List<long> UserIds, IToastNotification notificationObject);
        Task NotifyToastToAllUsersAsync(IToastNotification notificationObject);
        Task NotifyEventToUserAsync(long UserId, IEventNotification notificationObject);
        Task NotifyEventToUsersAsync(List<long> UserIds, IEventNotification notificationObject);
        Task NotifyEventToAllUsersAsync(IEventNotification notificationObject);
    }
}