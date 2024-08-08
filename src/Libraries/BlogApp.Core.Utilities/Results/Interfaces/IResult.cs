namespace BlogApp.Core.Utilities.Results.Interfaces;
public interface IResult
{
    bool IsSuccess { get; }
    string? Message { get; }
}

public interface IResult<T> : IResult
{
    T? Data { get; }
}
