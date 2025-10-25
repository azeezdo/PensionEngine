using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;

namespace PensionSystem.Infrastructure.Repositories;

public class MemberRepository: GenericRepository<Member>, IMemberRepository
{
    private readonly PensionDbContext _dbContext;
    public MemberRepository(PensionDbContext  dbContext): base(dbContext)
    {
        _dbContext = dbContext;
    }
}