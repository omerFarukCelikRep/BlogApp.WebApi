using BlogApp.Core.Utilities.Results.Interfaces;
using System.Text.Json.Serialization;

namespace BlogApp.Core.Utilities.Results.Concrete;
public class DataResult<T> : Result, IDataResult<T>
{
    public T? Data { get; init; }
    public DataResult(T? data, bool isSuccess) : base(isSuccess)
    {
        Data = data;
    }

    [JsonConstructor]
    public DataResult(T? data, bool isSuccess, string message) : base(isSuccess, message)
    {
        Data = data;
    }
}
