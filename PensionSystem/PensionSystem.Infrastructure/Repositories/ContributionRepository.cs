using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
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
    
    public async Task<IEnumerable<Contribution>> GetByMemberAsync(Guid memberId, CancellationToken ct = default)
    {
        return await _dbContext.Contributions.Where(c => c.MemberId == memberId).ToListAsync(ct);
    }

    public async Task<bool> ExistsMonthlyContributionForMemberInMonth(Guid memberId, DateTime dateInMonth, CancellationToken ct = default)
    {
        var start = new DateTime(dateInMonth.Year, dateInMonth.Month, 1);
        var end = start.AddMonths(1);
        return await _dbContext.Contributions.AnyAsync(c => c.MemberId == memberId && c.ContributionType == ContributionType.Monthly && c.ContributionDate >= start && c.ContributionDate < end, ct);
    }

    public async Task<decimal> SumContributions(Guid memberId, ContributionType? type, CancellationToken ct = default)
    {
        var q = _dbContext.Contributions.AsQueryable().Where(c => c.MemberId == memberId);
        if (type.HasValue) q = q.Where(c => c.ContributionType == type.Value);
        var sum = await q.SumAsync(c => (decimal?)c.Amount, ct);
        return sum ?? 0m;
    }
}