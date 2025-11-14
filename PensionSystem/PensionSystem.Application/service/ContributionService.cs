using Microsoft.EntityFrameworkCore;
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
    private readonly IContributionRepository _contributionRepository;

    public ContributionService(IUnitofWork uow, MonthlyContributionRule monthlyRule)
    {
        _uow = uow;
        _monthlyRule = monthlyRule;
    }

    public async Task<CustomResponse> AddContributionAsync(Contribution dto)
    {
        CustomResponse res = null;
        try
        {
            if (dto.ContributionType == ContributionType.Monthly)
            {
                var result = await _uow.contributionRepo.AddContributionAsync(dto);
                if (result == true)
                {
                    res = new CustomResponse(400, "Member already has a monthly contribution for this calendar month");
                }
                else
                {
                    var contribution = new Contribution(dto.MemberId, dto.ContributionType, dto.Amount, dto.ContributionDate,
                     dto.ReferenceNumber);
                    await _uow.contributionRepo.AddAsync(contribution);
                    await _uow.CompleteAsync();
                    res = new CustomResponse(200, "Contribution Successfully added");
                }
            }
            else 
            {
                var contribution = new Contribution(dto.MemberId, dto.ContributionType, dto.Amount, dto.ContributionDate,
                   dto.ReferenceNumber);
                await _uow.contributionRepo.AddAsync(contribution);
                await _uow.CompleteAsync();
                res = new CustomResponse(200, "Contribution Successfully added");
            } 
        }
        catch (Exception ex)
        {
            res = new CustomResponse(500, ex.Message);
        }
        return res;
    }

}
