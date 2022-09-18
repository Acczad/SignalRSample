using SignalR.BaseDto;

namespace SignalR
{
    public class NotifyUserCommand : Request<NotifyUserCommandResult>
    {
        public long ApplicationRef { get; set; }
        public string ApiURL { get; set; }
        public bool IsActive { get; set; }
        public bool IsPublic { get; set; }
        public bool IsRead { get; set; }

        public string _Title { get; set; }
    }
    public class NotifyUserCommandResult : SingleQueryResult<Result> { }
    public class Result
    {
    }
}
