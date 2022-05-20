using BlogApp.Core.DataAccess.Abstract;
using BlogApp.Core.Entities.Base;
using BlogApp.Core.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;

namespace BlogApp.Core.DataAccess.Base.EntityFramework.Repositories;
public class EfBaseRepository<TEntity, TContext> : IRepositoryAsync<TEntity>
    where TEntity : BaseEntity
    where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly ILogger<EfBaseRepository<TEntity, TContext>> _logger;
    protected readonly DbSet<TEntity> _table;

    public EfBaseRepository(TContext context, ILogger<EfBaseRepository<TEntity, TContext>> logger)
    {
        _context = context;
        _logger = logger;
        _table = _context.Set<TEntity>();
    }
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        try
        {
            await _table.AddAsync(entity);

            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);
            throw;
        }
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await _table.AnyAsync(expression);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        try
        {
            _table.Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);
            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true)
    {
        try
        {
            return tracking ? await _table.Where(x => x.Status != Status.Deleted)
                                          .ToListAsync() : await _table.Where(x => x.Status != Status.Deleted)
                                                                       .AsNoTracking()
                                                                       .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        try
        {
            return tracking ? await _table.Where(x => x.Status != Status.Deleted)
                                          .Where(expression)
                                          .ToListAsync() : await _table.Where(x => x.Status != Status.Deleted)
                                                                       .Where(expression)
                                                                       .AsNoTracking()
                                                                       .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        try
        {
            var data = tracking ? _table.Where(x => x.Status != Status.Deleted)
                                        : _table.Where(x => x.Status != Status.Deleted)
                                                .AsNoTracking();

            if (orderDesc)
            {
                return await data.OrderByDescending(orderby)
                                 .ToListAsync();
            }

            return await data.OrderBy(orderby)
                             .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderby, bool orderDesc = false, bool tracking = true)
    {
        try
        {
            var data = tracking ? _table.Where(x => x.Status != Status.Deleted)
                                        .Where(expression) : _table.Where(x => x.Status != Status.Deleted)
                                                                                        .Where(expression)
                                                                                        .AsNoTracking();
            if (orderDesc)
            {
                return await data.OrderByDescending(orderby)
                                 .ToListAsync();
            }

            return await data.OrderBy(orderby)
                             .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

    public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true)
    {
        try
        {
            return tracking ? await _table.FirstOrDefaultAsync(expression) : await _table.AsNoTracking().FirstOrDefaultAsync(expression);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

    public async Task<TEntity> GetByIdAsync(Guid id, bool tracking = true)
    {
        try
        {
            return tracking ? await _table.FindAsync(id) : await _table.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return entity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.InnerException, ex.Message);

            throw;
        }
    }

}
