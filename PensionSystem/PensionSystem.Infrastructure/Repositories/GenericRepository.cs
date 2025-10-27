using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

 public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly PensionDbContext _ctx;
       // protected readonly DbSet<T> _dbSet;
        public GenericRepository(PensionDbContext ctx)
        {
            _ctx = ctx;
            //_dbSet = ctx.Set<T>();
        }

        public virtual async Task AddAsync(T entity, CancellationToken ct = default) => await _ctx.AddAsync<T>(entity, ct);

        public virtual void Remove(T entity) => _ctx.Set<T>().Remove(entity);

        public virtual void Update(T entity) => _ctx.Set<T>().Update(entity);

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default) => await _ctx.Set<T>().FindAsync(new object[] { id }, ct) as T;

        public virtual async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>>? filter = null, CancellationToken ct = default)
        {
            IQueryable<T> q = _ctx.Set<T>();
            if (filter != null) q = q.Where(filter);
            return await q.ToListAsync(ct);
        }
        public async Task<T> GetByExpressionAsync(Expression<Func<T, bool>> expression)
        {
            return await _ctx.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }
    }
