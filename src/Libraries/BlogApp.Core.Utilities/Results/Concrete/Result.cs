using BlogApp.Core.Utilities.Results.Interfaces;

namespace BlogApp.Core.Utilities.Results.Concrete;
public record Result : IResult
{
    public bool IsSuccess { get; init; }

    public string Message { get; init; } = null!;

    public Error Error { get; init; } = null!;

    public static Result Success(string message = "") => new()
    {
        IsSuccess = true,
        Message = message,
        Error = Error.None
    };
    public static Result Failure(Error error) => new()
    {
        IsSuccess = false,
        Message = string.Empty,
        Error = error
    };
}

public record Result<T> : Result, IResult<T>
{
    public T? Data { get; init; }

    public static Result<T?> Success(T? data, string message = "") => new()
    {
        IsSuccess = true,
        Message = message,
        Error = Error.None,
        Data = data
    };

    public static new Result<T?> Failure(Error error) => new()
    {
        Error = error,
        IsSuccess = false,
        Message = string.Empty,
    };
}