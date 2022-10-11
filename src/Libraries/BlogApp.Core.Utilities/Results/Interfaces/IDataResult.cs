namespace BlogApp.Core.Utilities.Results.Interfaces;
public interface IDataResult<T> : IResult
{
    T Data { get; }
}
