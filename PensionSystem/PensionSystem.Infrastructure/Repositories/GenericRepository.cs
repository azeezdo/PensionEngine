using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

 public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PensionDbContext _ctx;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(PensionDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken ct = default) => await _dbSet.AddAsync(entity, ct);

        public virtual void Remove(T entity) => _dbSet.Remove(entity);

        public virtual void Update(T entity) => _dbSet.Update(entity);

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) => await _dbSet.FindAsync(new object[] { id }, ct) as T;

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default)
        {
            IQueryable<T> q = _dbSet;
            if (filter != null) q = q.Where(filter);
            return await q.ToListAsync(ct);
        }
        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.FirstOrDefaultAsync(expression);
        }
    }
