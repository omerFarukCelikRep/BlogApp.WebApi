using BlogApp.Core.Utilities.Results.Interfaces;

namespace BlogApp.Core.Utilities.Results.Concrete;
public record Result : IResult
{
    public bool IsSuccess { get; set; }

    public string Message { get; set; }
    public Result(bool success = false,string message = "")
    {
        IsSuccess = success;
        Message = message;
    }
}
