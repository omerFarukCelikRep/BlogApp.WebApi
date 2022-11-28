using BlogApp.Core.DataAccess.Interfaces.Models;

namespace BlogApp.Core.DataAccess.Models.Pagination;
public class Paginate<TModel> : IPaginate<TModel>
{
    internal Paginate(IEnumerable<TModel> source, int index, int size, int from)
    {
        var enumerable = source as TModel[] ?? source.ToArray();

        if (from > index)
            throw new ArgumentException($"indexFrom: {from} > pageIndex: {index}, must indexFrom <= pageIndex");

        if (source is IQueryable<TModel> querable)
        {
            Index = index;
            Size = size;
            From = from;
            Count = querable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);

            Items = querable.Skip((Index - From) * Size).Take(Size).ToList().AsReadOnly();
        }
        else
        {
            Index = index;
            Size = size;
            From = from;

            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);

            Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
        }
    }
    public int From { get; init; }
    public int Index { get; init; }
    public int Size { get; init; }
    public int Count { get; init; }
    public int Pages { get; init; }
    public IReadOnlyCollection<TModel> Items { get; init; }
    public bool HasPrevious => Index - From > 0;
    public bool HasNext => Index - From + 1 < Pages;
}
