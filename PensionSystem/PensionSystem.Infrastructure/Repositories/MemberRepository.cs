using Microsoft.EntityFrameworkCore;
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
    
    public async Task<Member?> GetByEmailAsync(string email, CancellationToken ct = default)
    {
        return await _dbContext.Members.FirstOrDefaultAsync(m => m.Email == email, ct);
    }

    public async Task<IEnumerable<Member>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct = default)
    {
        return await _dbContext.Members.Where(m => ids.Contains(m.Id)).ToListAsync(ct);
    }
}