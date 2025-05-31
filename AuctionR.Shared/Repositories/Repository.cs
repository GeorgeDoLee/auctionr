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

    public async Task AddAsync(TEntity entity, CancellationToken ct)
    {
        await _context.Set<TEntity>()
            .AddAsync(entity, ct);
    }

    public async Task<IEnumerable<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>> predicate, CancellationToken ct)
    {
        return await _context.Set<TEntity>()
            .Where(predicate)
            .ToListAsync(ct);
    }

    public async Task<TEntity?> GetAsync(int id, CancellationToken ct)
    {
        return await _context.Set<TEntity>()
            .FindAsync(id, ct);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>()
            .ToListAsync();
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>()
            .Remove(entity);
    }

    public IQueryable<TEntity> Query(
        Expression<Func<TEntity, bool>>? expression)
    {
        return expression == null ?
            _context.Set<TEntity>().AsQueryable() :
            _context.Set<TEntity>().Where(expression);
    }
}