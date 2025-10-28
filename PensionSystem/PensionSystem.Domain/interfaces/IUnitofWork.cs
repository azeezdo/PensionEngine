namespace PensionSystem.Domain.interfaces;

public interface IUnitofWork 
{
    IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
    Task<int> CompleteAsync();

    IMemberRepository memberRepo { get; }
    IContributionRepository contributionRepo { get; }
    IEmployerRepository employerRepo { get; }
    IBenefitRepository benefitRepo { get; }
    ITransactionHistoryRepository transactionRepo { get; }
   
}

