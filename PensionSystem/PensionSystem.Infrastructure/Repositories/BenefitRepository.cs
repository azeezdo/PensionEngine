using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

public class BenefitRepository: GenericRepository<Benefit>, IBenefitRepository
{
    private readonly PensionDbContext _dbContext;
    public BenefitRepository(PensionDbContext  dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }
    
}