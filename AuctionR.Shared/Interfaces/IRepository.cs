using System.Linq.Expressions;

namespace AuctionR.Shared.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetAsync(int id, CancellationToken ct = default);

    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate, CancellationToken ct = default);

    Task AddAsync(T entity, CancellationToken ct = default);

    void Remove(T entity);

    IQueryable<T> Query(Expression<Func<T, bool>>? expression);
}
