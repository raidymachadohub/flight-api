using Flight.Shared.FlowControl.Enum;

namespace Flight.Shared.FlowControl.Model;

public class Error
{
    public string Message { get; set; } = null!;
    public ErrorType ErrorType { get; set; }

    public Error(ErrorType errorType, string message)
    {
        ErrorType = errorType;
        Message = message;
    }
    
    public Error(string message)
    {
        Message = message;
    }
    public Error(){}
}