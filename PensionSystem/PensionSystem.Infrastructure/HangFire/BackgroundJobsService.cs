using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;
namespace PensionSystem.Infrastructure.HangFire;

public class BackgroundJobsService
{
    private readonly IUnitofWork _uow;
    private readonly ILogger<BackgroundJobsService> _log;
    public readonly PensionDbContext _context;

    public BackgroundJobsService(IUnitofWork uow, ILogger<BackgroundJobsService> log)
    {
        _uow = uow;
        _log = log;
    }

    public async Task RunMonthlyValidation()
    {
        try
        {
            var invalidContributions = await _context.Contributions
    .GroupBy(c => new { c.MemberId, Month = c.ContributionDate.Month, Year = c.ContributionDate.Year })
    .Where(g => g.Count(c => c.ContributionType == Domain.Enums.ContributionType.Monthly) > 1)
    .ToListAsync();

            if (invalidContributions.Any())
            {
                // Save report or log notification
                _log.LogInformation($"Found {invalidContributions.Count} duplicate monthly contributions.");
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "RunMonthlyValidation failed");
            throw;
        }
    }

    public async Task UpdateBenefitEligibility()
    {
        try
        {
            var members = await _context.Members
            .Include(m => m.Contributions)
            .ToListAsync();

            foreach (var member in members)
            {
                var totalMonths = member.Contributions
                    .Where(c => c.ContributionType == Domain.Enums.ContributionType.Monthly)
                    .Select(c => new { c.ContributionDate.Year, c.ContributionDate.Month })
                    .Distinct()
                    .Count();

                var eligible = totalMonths >= 12; // e.g., 12-month minimum
                member.IsBenefitEligible = eligible;
            }
            await _context.SaveChangesAsync();
            _log.LogInformation("Updating benefit eligibility Successful");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "UpdateBenefitEligibility failed");
            throw;
        }
    }

    public async Task ApplyMonthlyInterest()
    {
        try
        {
            var contributions = await _context.Contributions.ToListAsync();
            foreach (var contribution in contributions)
            {
                // Simple interest example: 1% per month
                var interest = contribution.Amount * 0.01m;
                contribution.Amount += interest;
            }
            await _context.SaveChangesAsync();
            _log.LogInformation("Applying monthly interest");
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "ApplyMonthlyInterest failed");
            throw;
        }
    }

    public async Task GenerateMemberStatements()
    {
        try
        {
            var members = await _context.Members.Include(m => m.Contributions).ToListAsync();

            foreach (var member in members)
            {
                var total = member.Contributions.Sum(c => c.Amount);
                _log.LogInformation($"Statement generated for {member.FirstName}: Total = {total:C}");
            }
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "GenerateMemberStatements failed");
            throw;
        }
    }

}
