namespace PensionSystem.Domain.interfaces;

public interface IUnitofWork :IDisposable
{
    IMemberRepository Members { get; }
    IContributionRepository Contributions { get; }
    IEmployerRepository Employers { get; }
    ITransactionHistoryRepository TransactionHistories { get; }
    IBenefitRepository Benefits { get; }
    Task<int> CommitAsync(CancellationToken ct = default);
}

