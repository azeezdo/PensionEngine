using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

public class EmployerRepository: GenericRepository<Employer>, IEmployerRepository
{
    private readonly PensionDbContext _dbContext;
    public EmployerRepository(PensionDbContext  dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }
}
