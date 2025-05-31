using System.Linq.Expressions;

namespace AuctionR.Shared.Interfaces;

public interface IRepository<T>
{
    Task<T?> GetAsync(int id, CancellationToken ct);

    Task<IEnumerable<T>> GetAllAsync(
        int pageNumber, int pageSize, CancellationToken ct);

    Task<IEnumerable<T>> FindAsync(
        Expression<Func<T, bool>> predicate, CancellationToken ct);

    Task AddAsync(T entity, CancellationToken ct);

    void Remove(T entity);

    IQueryable<T> Query(Expression<Func<T, bool>>? expression);
}
