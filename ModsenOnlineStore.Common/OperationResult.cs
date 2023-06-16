namespace ModsenOnlineStore.Common;

public class OperationResult
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }

    public OperationResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }
}