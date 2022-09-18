namespace SignalR.BaseDto
{
    public abstract class Request<TResult>
        where TResult : ResultBase
    {
    }

    public class Result : ResultBase
    {
    }
}
