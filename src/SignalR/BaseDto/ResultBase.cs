namespace SignalR.BaseDto
{
    public abstract class ResultBase
    {
        public bool Success { get; set; } = true;
        public string ErrorMessage { get; set; }
        public int ErrorCode { get; set; }
    }
}
