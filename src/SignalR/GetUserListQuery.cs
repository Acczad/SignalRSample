using SignalR.BaseDto;

namespace SignalR
{
    public class GetUserListQuery : Query
    {
    }
    public class GetUserListQueryResult : ListQueryResult<GetUserListQueryModel> { }
    public class GetUserListQueryModel
    {
        public GetUserListQueryModel()
        {
            ConnectionId = new List<string>();
        }
        public long UserId { get; set; }
        public DateTime ConnectDateTime { get; set; }
        public List<string> ConnectionId { get; set; }
    }
}
