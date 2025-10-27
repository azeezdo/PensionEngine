using System.Linq.Expressions;

namespace PensionSystem.Domain.interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IReadOnlyList<T>> GetAllAsync();

    Task<T> GetByIdAsync(Guid id);

    Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression);

    Task DeleteAsync(T entity);

    Task UpdateAsync(T entity);

    Task AddAsync(T entity);

    void Update(T entity);

    void Delete(T entity);
}