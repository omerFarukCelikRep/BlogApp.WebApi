using BlogApp.Core.DataAccess.Extensions;
using BlogApp.Core.DataAccess.Interfaces.Models;
using BlogApp.Core.DataAccess.Interfaces.Repositories;
using BlogApp.Core.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
public class EFBaseRepository<TEntity> : IAsyncPaginateRepository<TEntity>, IAsyncFindableRepository<TEntity>, IAsyncOrderableRepository<TEntity>, IAsyncQueryableRepository<TEntity>, IAsyncInsertableRepository<TEntity>, IAsyncUpdateableRepository<TEntity>, IAsyncDeleteableRepository<TEntity>, IDeleteableRepository<TEntity>, IAsyncRepository
    where TEntity : BaseEntity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _table;
    public EFBaseRepository(DbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="TEntity"/>
    /// </returns>
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _table.AddAsync(entity, cancellationToken);
        return entity;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="bool"/>
    /// </returns>
    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? expression = null, CancellationToken cancellationToken = default)
    {
        return expression is null
            ? GetAll().AnyAsync(cancellationToken)
            : GetAll().AnyAsync(expression, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled(cancellationToken)
            : Task.FromResult(Delete);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true, CancellationToken cancellationToken = default)
    {
        return await GetAll(tracking).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default)
    {
        return await GetAll(tracking).Where(expression)
                                     .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="orderby"></param>
    /// <param name="orderDesc"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var values = GetAll(tracking);

        return !orderDesc
            ? await values.OrderBy(orderby).ToListAsync(cancellationToken)
            : await values.OrderByDescending(orderby).ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="expression"></param>
    /// <param name="orderby"></param>
    /// <param name="orderDesc"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var values = GetAll(tracking)
                              .Where(expression);

        return !orderDesc
            ? await values.OrderBy(orderby)
                          .ToListAsync(cancellationToken)
            : await values.OrderByDescending(orderby)
                          .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="TEntity"/>
    /// </returns>
    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true, CancellationToken cancellationToken = default)
    {
        return GetAll(tracking).FirstOrDefaultAsync(expression, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="TEntity"/>
    /// </returns>
    public Task<TEntity?> GetByIdAsync(Guid id, bool tracking = true, CancellationToken cancellationToken = default)
    {
        return GetAll(tracking).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="TEntity"/>
    /// </returns>
    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        return cancellationToken.IsCancellationRequested
            ? Task.FromCanceled<TEntity>(cancellationToken)
            : Task.FromResult(_table.Update(entity).Entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="int"/>
    /// </returns>
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="orderby"></param>
    /// <param name="orderDesc"></param>
    /// <param name="takeCount"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var values = GetAll(tracking);

        values = !orderDesc
            ? values.OrderBy(orderby)
            : values.OrderByDescending(orderby);

        return takeCount > 0
            ? await values.Take(takeCount)
                          .ToListAsync(cancellationToken)
            : await values.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="expression"></param>
    /// <param name="orderby"></param>
    /// <param name="orderDesc"></param>
    /// <param name="takeCount"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IEnumerable{TEntity}"/>
    /// </returns>
    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, int takeCount = 0, bool tracking = true, CancellationToken cancellationToken = default)
    {
        var values = GetAll(tracking).Where(expression);

        values = !orderDesc
            ? values.OrderBy(orderby)
            : values.OrderByDescending(orderby);

        return takeCount > 0
            ? await values.Take(takeCount)
                          .ToListAsync(cancellationToken)
            : await values.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="entity">
    /// <see cref="TEntity"/>
    /// </param>
    public void Delete(TEntity entity) => _table.Remove(entity);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="size"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IPaginate{TEntity}"/>
    /// </returns>
    public Task<IPaginate<TEntity>> GetAllAsPaginateAsync(int index = 0, int size = 10, bool tracking = true, CancellationToken cancellationToken = default)
    {
        return GetAll(tracking).ToPaginateAsync(index, size, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="index"></param>
    /// <param name="size"></param>
    /// <param name="tracking"></param>
    /// <param name="cancellationToken"><see cref="CancellationToken"/></param>
    /// <returns>
    /// <see cref="IPaginate{TEntity}"/>
    /// </returns>
    public Task<IPaginate<TEntity>> GetAllAsPaginateAsync(Expression<Func<TEntity, bool>> expression, int index = 0, int size = 10, bool tracking = true, CancellationToken cancellationToken = default)
    {
        return GetAll(tracking).Where(expression)
                               .ToPaginateAsync(index, size, cancellationToken);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tracking"></param>
    /// <returns><see cref="IQueryable{TEntity}"/></returns>
    protected IQueryable<TEntity> GetAll(bool tracking = true)
    {
        var values = _table.AsQueryable<TEntity>();

        return tracking
            ? values
            : values.AsNoTracking();
    }
}