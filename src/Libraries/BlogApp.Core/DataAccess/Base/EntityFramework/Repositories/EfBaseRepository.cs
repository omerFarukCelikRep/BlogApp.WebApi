﻿using BlogApp.Core.DataAccess.Extensions;
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
        var values = GetAllActives();
        return tracking ? await values.ToListAsync() : await values.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        var values = GetAllActives()
                             .Where(expression);

        return tracking ? await values.ToListAsync() : await values.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = tracking ? GetAllActives() : GetAllActives().AsNoTracking();

        return !orderDesc ? await values.OrderBy(orderby).ToListAsync() : await values.OrderByDescending(orderby).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        var values = _table
                              .Where(x => x.Status != Status.Deleted)
                              .Where(expression);

        values = tracking ? values : values.AsNoTracking();

        return !orderDesc ? await values.OrderBy(orderby).ToListAsync() : await values.OrderByDescending(orderby).ToListAsync();
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        return tracking ? GetAllActives().FirstOrDefaultAsync(expression) : GetAllActives().AsNoTracking().FirstOrDefaultAsync(expression);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true)
    {
        return tracking ? GetAllActives().FirstOrDefaultAsync(x => x.Id == id) : GetAllActives().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public Task<TEntity> UpdateAsync(TEntity entity)
    {
        return Task.FromResult(_table.Update(entity).Entity);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true)
    {
        var values = tracking ? GetAllActives() : GetAllActives().AsNoTracking();

        values = !orderDesc ? values.OrderBy(orderby) : values.OrderByDescending(orderby);

        return takeCount > 0 ? await values.Take(takeCount).ToListAsync() : await values.ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true)
    {
        var values = GetAllActives()
                             .Where(expression);

        values = tracking ? values : values.AsNoTracking();
        values = !orderDesc ? values.OrderBy(orderby) : values.OrderByDescending(orderby);

        return takeCount > 0 ? await values.Take(takeCount).ToListAsync() : await values.ToListAsync();
    }

    private IQueryable<TEntity> GetAllActives()
    {
        return _table.Where(x => x.Status != Status.Deleted);
    }

    public Task<IPaginate<TEntity>> GetAllAsPaginateAsync(int index = 0, int size = 10, bool tracking = true)
    {
        var items = GetAllActives();
        if (!tracking)
        {
            items.AsNoTracking();
        }

        return items.ToPaginateAsync(index, size);
    }

    public Task<IPaginate<TEntity>> GetAllAsPaginateAsync(Expression<Func<TEntity, bool>> expression, int index = 0, int size = 10, bool tracking = true)
    {
        var items = GetAllActives().Where(expression);
        if (!tracking)
        {
            items.AsNoTracking();
        }

        return items.ToPaginateAsync(index, size);
    }
}
