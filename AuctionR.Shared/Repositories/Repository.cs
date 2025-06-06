using AuctionR.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AuctionR.Shared.Repositories;

public class Repository<TContext, TEntity> : IRepository<TEntity>
    where TEntity : class
    where TContext : DbContext
{
    protected readonly TContext _context;

    public Repository(TContext context)
    {
        _context = context;
    }

    public virtual async Task AddAsync(TEntity entity, CancellationToken ct = default)
    {
        await _context.Set<TEntity>()
            .AddAsync(entity, ct);
    }

    public virtual async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken ct = default)
    {
        return await _context.Set<TEntity>()
            .Where(predicate)
            .ToListAsync(ct);
    }

    public virtual async Task<TEntity?> GetAsync(int id, CancellationToken ct = default)
    {
        return await _context.Set<TEntity>()
            .FindAsync(id, ct);
    }

    public virtual async Task<IEnumerable<TEntity>> GetPagedAsync(
        int pageNumber, 
        int pageSize, 
        IQueryable<TEntity>? queryabe = null, 
        CancellationToken ct = default)
    {
        var query = queryabe == null
            ? _context.Set<TEntity>().AsQueryable()
            : queryabe;

        return await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);
        
    }

    public virtual void Remove(TEntity entity)
    {
        _context.Set<TEntity>()
            .Remove(entity);
    }

    public virtual IQueryable<TEntity> Query(
        Expression<Func<TEntity, bool>>? expression)
    {
        return expression == null ?
            _context.Set<TEntity>().AsQueryable() :
            _context.Set<TEntity>().Where(expression);
    }
}