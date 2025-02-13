namespace Flight.Shared.FlowControl.Model;

public class Result
{
    public Error? Error { get; set; }
    public bool Success { get; set; }

    public Result(Error? error, bool success)
    {
        Error = error;
        Success = success;
    }

    private Result(Error? error)
    {
        Error = error;
    }

    private Result()
    {
    }

    public static Result Fail(Error? error) => new Result(error, false);

    public static Result<T> Fail<T>(Error? error) =>
        new Result<T>(default!, error, false);

    public static Result Ok() => new Result(null, true);

    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, new Error(), true);
    }
}

public class Result<T> : Result
{
    protected internal Result(T value, Error? error, bool success)
        : base(error, success)
    {
        Value = value;
    }

    public T Value { get; set; } 
}