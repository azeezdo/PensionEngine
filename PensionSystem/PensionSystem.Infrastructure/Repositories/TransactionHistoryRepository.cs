using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

public class TransactionHistoryRepository : GenericRepository<TransactionHistory>, ITransactionHistoryRepository
{
    private readonly PensionDbContext _dbContext;
    public TransactionHistoryRepository(PensionDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
}