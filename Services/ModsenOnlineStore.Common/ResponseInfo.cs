namespace ModsenOnlineStore.Common
{
    public class ResponseInfo<T>
    {
        public ResponseInfo(T data, bool success, string message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}