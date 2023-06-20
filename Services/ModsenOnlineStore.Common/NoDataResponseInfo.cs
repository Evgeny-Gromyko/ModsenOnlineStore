namespace ModsenOnlineStore.Common
{
    public class NoDataResponseInfo
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public NoDataResponseInfo(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
