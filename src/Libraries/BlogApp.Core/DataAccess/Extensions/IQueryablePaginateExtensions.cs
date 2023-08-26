using BlogApp.Core.DataAccess.Interfaces.Models;
using BlogApp.Core.DataAccess.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Core.DataAccess.Extensions;
public static class IQueryablePaginateExtensions
{
    public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(source);
        EnsureInRange(index, size);

        int count = await source.CountAsync(cancellationToken)
                                .ConfigureAwait(false);

        var items = await source.Skip(index * size)
                                .Take(index)
                                .ToListAsync(cancellationToken: cancellationToken);

        return new Paginate<T>(items, index, size, count);
    }

    public static IPaginate<T> ToPaginate<T>(this IQueryable<T> source, int index = 0, int size = 10)
    {
        ArgumentNullException.ThrowIfNull(source);

        EnsureInRange(index, size);

        int count = source.Count();
        var items = source.Skip(index * size)
                          .Take(index)
                          .ToList();

        return new Paginate<T>(items, index, size, count);
    }

    private static void EnsureInRange(int index, int size)
    {
        if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

        if (size < 0)
            throw new ArgumentOutOfRangeException(nameof(size));
    }
}