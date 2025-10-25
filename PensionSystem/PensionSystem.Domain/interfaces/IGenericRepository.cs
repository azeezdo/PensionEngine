using System.Linq.Expressions;

namespace PensionSystem.Domain.interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task AddAsync(T entity, CancellationToken ct = default);
    void Update(T entity);
    void Remove(T entity);
    Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default);
    Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression);
}