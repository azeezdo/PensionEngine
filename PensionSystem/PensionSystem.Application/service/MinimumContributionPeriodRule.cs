using PensionSystem.Domain.interfaces;

namespace PensionSystem.Application.service;

public class MinimumContributionPeriodRule
{
    private readonly IUnitofWork _uow;
    private readonly int _minimumMonths;
    public MinimumContributionPeriodRule(IUnitofWork uow, int minimumMonths = 12)
    {
        _uow = uow;
        _minimumMonths = minimumMonths;
    }

    public async Task<bool> IsEligibleForBenefits(Guid memberId)
    {
        var sum = await _uow.contributionRepo.SumContributions(memberId, null);
        return sum > 0; // simplified; extend to check distinct months
    }
}