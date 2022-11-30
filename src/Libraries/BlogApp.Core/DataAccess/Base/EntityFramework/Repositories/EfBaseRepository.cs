using BlogApp.Core.DataAccess.Extensions;
using BlogApp.Core.DataAccess.Interfaces.Models;
using BlogApp.Core.DataAccess.Interfaces.Repositories;
using BlogApp.Core.Entities.Base;
using BlogApp.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
public class EfBaseRepository<TEntity> : IAsyncPaginateRepository<TEntity>, IAsyncFindableRepository<TEntity>, IAsyncOrderableRepository<TEntity>, IAsyncQueryableRepository<TEntity>, IAsyncInsertableRepository<TEntity>, IAsyncUpdateableRepository<TEntity>, IAsyncDeleteableRepository<TEntity>, IAsyncRepository
    where TEntity : BaseEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _table;

    public EfBaseRepository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _table.AddAsync(entity);
        return entity;
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null)
    {
        return expression is null ? GetAllActives().AnyAsync() : GetAllActives().AnyAsync(expression);
    }

    public Task DeleteAsync(TEntity entity)
    {
        return Task.FromResult(_table.Remove(entity));
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
    {
        return await GetAllActives(tracking).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return await GetAllActives(tracking).Where(expression).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = GetAllActives(tracking);

        return !orderDesc ? await values.OrderBy(orderby).ToListAsync() : await values.OrderByDescending(orderby).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = GetAllActives(tracking)
                              .Where(expression);

        return !orderDesc ? await values.OrderBy(orderby).ToListAsync() : await values.OrderByDescending(orderby).ToListAsync();
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return GetAllActives(tracking).FirstOrDefaultAsync(expression);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
    {
        return GetAllActives(tracking).FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(_table.Update(entity).Entity);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true)
    {
        var values = GetAllActives(tracking);

        values = !orderDesc ? values.OrderBy(orderby) : values.OrderByDescending(orderby);

        return takeCount > 0 ? await values.Take(takeCount).ToListAsync() : await values.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true)
    {
        var values = GetAllActives(tracking)
                             .Where(expression);

        values = !orderDesc ? values.OrderBy(orderby) : values.OrderByDescending(orderby);

        return takeCount > 0 ? await values.Take(takeCount).ToListAsync() : await values.ToListAsync();
    }   

    public Task<IPaginate<TEntity>> GetAllAsPaginateAsync(int index = 0, int size = 10, bool tracking = true)
    {
        return GetAllActives(tracking).ToPaginateAsync(index, size);
    }

    public Task<IPaginate<TEntity>> GetAllAsPaginateAsync(Expression<Func<TEntity, bool>> expression, int index = 0, int size = 10, bool tracking = true)
    {
        return GetAllActives(tracking).Where(expression).ToPaginateAsync(index, size);
    }

    protected IQueryable<TEntity> GetAllActives(bool tracking = true)
    {
        var values = _table.Where(x => x.Status != Status.Deleted);

        return tracking ? values : values.AsNoTracking();
    }
}
