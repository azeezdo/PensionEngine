using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Domain.Models;

namespace PensionSystem.Application.service;

public class ContributionService : IContributionService
{
    private readonly IUnitofWork _uow;
    private readonly MonthlyContributionRule _monthlyRule;

    public ContributionService(IUnitofWork uow, MonthlyContributionRule monthlyRule)
    {
        _uow = uow;
        _monthlyRule = monthlyRule;
    }

    public async Task<CustomResponse> AddContributionAsync(CreateContributionDto dto)
    {
        CustomResponse res = null;
        try
        {
            if (dto.ContributionType == ContributionType.Monthly)
            {
                var result  = await _monthlyRule.CanAddMonthlyContributionAsync(dto.MemberId, dto.ContributionDate);
                if (!result)
                {
                    res = new CustomResponse(400, "Member already has a monthly contribution for this calendar month");
                }
            }

            var contribution = new Contribution(dto.MemberId, dto.ContributionType, dto.Amount, dto.ContributionDate,
                dto.ReferenceNumber);
            await _uow.contributionRepo.AddAsync(contribution);
            await _uow.CompleteAsync();
           res =  new CustomResponse(200, "Contribution Successfully added");
        }
        catch (Exception e)
        {
            
        }
        return res;
    }
}
