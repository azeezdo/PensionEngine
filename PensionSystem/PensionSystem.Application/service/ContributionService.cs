using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
using PensionSystem.Domain.interfaces;

namespace PensionSystem.Application.service;

public class ContributionService
{
    private readonly IUnitofWork _uow;
    private readonly MonthlyContributionRule _monthlyRule;

    public ContributionService(IUnitofWork uow, MonthlyContributionRule monthlyRule)
    {
        _uow = uow;
        _monthlyRule = monthlyRule;
    }

    public async Task<Guid> AddContributionAsync(CreateContributionDto dto)
    {
        if (dto.ContributionType == ContributionType.Monthly)
        {
            var ok = await _monthlyRule.CanAddMonthlyContributionAsync(dto.MemberId, dto.ContributionDate);
            if (!ok) throw new InvalidOperationException("Member already has a monthly contribution for this calendar month.");
        }

        var c = new Contribution(dto.MemberId, dto.ContributionType, dto.Amount, dto.ContributionDate, dto.ReferenceNumber);
        await _uow.Contributions.AddAsync(c);
        await _uow.CommitAsync();
        return c.Id;
    }
}
