namespace SignalR.BaseDto
{
    public class SingleQueryResult<TEntity> : QueryResult
    {
        public TEntity Entity { get; set; }
    }
}
