namespace SignalR.BaseDto
{
    public interface IPageable
    {
        int Page { get; set; }
        int Take { get; set; }
        bool DisablePaging { get; set; }
    }

    public class Query 
    {
        public int DefaultTake { get; protected set; } = 20;

        private int _take;
        public int Take
        {
            get => _take;
            set => _take = value < 0 ? DefaultTake : value;
        }


        private int _page = 0;
        public int Page
        {
            get => _page;
            set => _page = value;
        }
    }
}
