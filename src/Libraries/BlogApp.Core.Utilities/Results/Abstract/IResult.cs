namespace BlogApp.Core.Utilities.Results.Abstract;
public interface IResult
{
    bool IsSuccess { get; }
    string? Message { get; }
}
