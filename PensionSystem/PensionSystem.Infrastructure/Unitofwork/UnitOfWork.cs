using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;
using PensionSystem.Infrastructure.Repositories;
using System.Collections;

namespace PensionSystem.Infrastructure.Unitofwork;

public class UnitOfWork : IUnitofWork
{
    private readonly PensionDbContext _ctx;
    private MemberRepository? _memberRepo;
    private ContributionRepository? _contributionRepo;
    private EmployerRepository? _employerRepo;
    private BenefitRepository? _benefitRepo;
    private  TransactionHistoryRepository? _transactionRepo;
    

   /* public UnitOfWork(PensionDbContext ctx) => _ctx = ctx;
    private Hashtable _repositories;
    public DatabaseFacade Database => _ctx.Database;*/

    public IMemberRepository Members => _memberRepo ??= new MemberRepository(_ctx);
    public IContributionRepository Contributions => _contributionRepo ??= new ContributionRepository(_ctx);
    public IEmployerRepository Employers => _employerRepo ??= new EmployerRepository(_ctx);
    public IBenefitRepository Benefits => _benefitRepo ??= new BenefitRepository(_ctx);
    public ITransactionHistoryRepository TransactionHistories => _transactionRepo ??= new TransactionHistoryRepository(_ctx);

    public void Dispose() => _ctx.Dispose();

    public Task<int> CommitAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);

    /* public IGenericRepository<TEntity> repository<TEntity>() where TEntity : class
     {
         if (_repositories == null) _repositories = new Hashtable();
         var Type = typeof(TEntity).Name;
         if (!_repositories.ContainsKey(Type))
         {
             var repositoryType = typeof(GenericRepository<TEntity>);
             var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _ctx);
             _repositories.Add(Type, repositoryInstance);
         }
         return (IGenericRepository<TEntity>)_repositories[Type];
     }

     public async Task<int> CompleteAsync()
     {
         return await _ctx.SaveChangesAsync();
     }*/

    /* public DapperGenericRepository<T> GetRepository<T>() where T : class
     {
         throw new NotImplementedException();
     }*/
}
