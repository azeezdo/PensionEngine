using PensionSystem.Domain.interfaces;

namespace PensionSystem.Application.service;

public class MonthlyContributionRule
{
    private readonly IUnitofWork _uow;
    public MonthlyContributionRule(IUnitofWork uow) => _uow = uow;

    public async Task<bool> CanAddMonthlyContributionAsync(Guid memberId, DateTime date)
    {
        // check repository for existing monthly contribution in same calendar month
        return !await _uow.Contributions.ExistsMonthlyContributionForMemberInMonth(memberId, date);
    }
}