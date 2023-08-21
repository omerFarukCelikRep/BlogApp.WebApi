namespace BlogApp.Core.Utilities.Results.Concrete;
public record ErrorResult : Result
{
    public ErrorResult() : base(false) { }

    public ErrorResult(string message) : base(false, message) { }
}