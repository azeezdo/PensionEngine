using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

public class ContributionRepository: GenericRepository<Contribution>, IContributionRepository
{
    private readonly PensionDbContext _dbContext;
    public ContributionRepository(PensionDbContext  dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }
}