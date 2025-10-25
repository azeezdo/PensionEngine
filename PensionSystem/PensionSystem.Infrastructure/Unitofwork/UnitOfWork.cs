using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;
using PensionSystem.Infrastructure.Repositories;

namespace PensionSystem.Infrastructure.Unitofwork;

public class UnitOfWork : IUnitofWork
{
    private readonly PensionDbContext _ctx;
    private MemberRepository? _memberRepo;
    private ContributionRepository? _contributionRepo;
    private EmployerRepository? _employerRepo;
    private BenefitRepository? _benefitRepo;
    private  TransactionHistoryRepository? _transactionRepo;
    

    public UnitOfWork(PensionDbContext ctx) => _ctx = ctx;

    public IMemberRepository Members => _memberRepo ??= new MemberRepository(_ctx);
    public IContributionRepository Contributions => _contributionRepo ??= new ContributionRepository(_ctx);
    public IEmployerRepository Employers => _employerRepo ??= new EmployerRepository(_ctx);
    public IBenefitRepository Benefits => _benefitRepo ??= new BenefitRepository(_ctx);
    public ITransactionHistoryRepository TransactionHistories => _transactionRepo ??= new TransactionHistoryRepository(_ctx);

    public void Dispose() => _ctx.Dispose();

    public Task<int> CommitAsync(CancellationToken ct = default) => _ctx.SaveChangesAsync(ct);
}
