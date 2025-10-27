namespace PensionSystem.Domain.interfaces;

public interface IUnitofWork 
{
   /* IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();*/
    //DapperGenericRepository<T> GetRepository<T>() where T : class;
    IMemberRepository Members { get; }
    IContributionRepository Contributions { get; }
    IEmployerRepository Employers { get; }
    ITransactionHistoryRepository TransactionHistories { get; }
    IBenefitRepository Benefits { get; }
    Task<int> CommitAsync(CancellationToken ct = default);
}

