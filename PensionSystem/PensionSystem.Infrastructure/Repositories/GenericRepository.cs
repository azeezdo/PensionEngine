using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

 public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PensionDbContext _dbContext;
        public GenericRepository(PensionDbContext ctx)
        {
        _dbContext = ctx;
        }

    public async Task AddAsync(T entity)
    {
        await _dbContext.AddAsync<T>(entity);
    }
    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }
    public async Task DeleteAsync(T entity)
    {
        await Task.Run(() => _dbContext.Set<T>().Remove(entity));
    }
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _dbContext.Set<T>().ToListAsync();
    }
    public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression)
    {
        return await _dbContext.Set<T>().FirstOrDefaultAsync(expression);
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }
    public void Update(T entity)
    {
        _dbContext.Attach<T>(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
    public async Task UpdateAsync(T entity)
    {
        await Task.Run(() => _dbContext.Attach<T>(entity));
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
}
