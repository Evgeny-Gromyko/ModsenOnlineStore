namespace ModsenOnlineStore.Common
{
    public class DataResponseInfo<T> : ResponseInfo
    {
        public DataResponseInfo(T data, bool success, string message) : base(success, message)
        {
            Data = data;
            Success = success;
            Message = message;
        }
        
        public T Data { get; set; }
    }
}