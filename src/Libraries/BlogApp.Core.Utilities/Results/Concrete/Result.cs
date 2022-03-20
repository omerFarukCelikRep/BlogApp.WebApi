using BlogApp.Core.Utilities.Results.Abstract;

namespace BlogApp.Core.Utilities.Results.Concrete;
public class Result : IResult
{
    public bool IsSuccess { get; set; }

    public string? Message { get; set; }

    public Result()
    {
        IsSuccess = false;
        Message = string.Empty;
    }

    public Result(bool isSuccess) : this()
    {
        IsSuccess = isSuccess;
    }

    public Result(bool isSuccess, string message) : this(isSuccess)
    {
        Message = message;
    }
}
