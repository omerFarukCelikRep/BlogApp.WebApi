using BlogApp.Core.DataAccess.Interfaces.Models;

namespace BlogApp.Core.DataAccess.Models.Pagination;
public class Paginate<TModel> : IPaginate<TModel>
{
    internal Paginate(IEnumerable<TModel> source, int index, int size)
    {
        if (source is IQueryable<TModel> querable)
        {
            Index = index;
            Size = size;
            Count = querable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);

            Items = querable.Skip(Index * Size).Take(Size).ToList().AsReadOnly();
        }
        else
        {
            var enumerable = source as TModel[] ?? source.ToArray();
            Index = index;
            Size = size;
            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = enumerable.Skip(Index * Size).Take(Size).ToList();
        }
    }
    internal Paginate(IEnumerable<TModel> source, int index, int size, int count) : this(source, index, size)
    {
        Count = count;
    }
    public int Index { get; private set; }
    public int Size { get; private set; }
    public int Count { get; private set; }
    public int Pages { get; private set; }
    public IReadOnlyCollection<TModel> Items { get; init; }
    public bool HasPrevious => Index * Size > 0;
    public bool HasNext => Index < Pages;
}
