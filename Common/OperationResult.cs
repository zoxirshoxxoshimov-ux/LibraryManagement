namespace LibraryManagement.Common;

public class OperationResult
{
    public bool IsSuccess { get; }
    public string Message { get; }

    public OperationResult(bool isSuccess, string message)
    {
        this.IsSuccess = isSuccess;
        this.Message = message;
    }
}