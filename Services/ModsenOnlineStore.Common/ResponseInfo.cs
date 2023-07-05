namespace ModsenOnlineStore.Common;

public class ResponseInfo
{
    public bool Success { get; set; }
    
    public string Message { get; set; }

    public ResponseInfo(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}
