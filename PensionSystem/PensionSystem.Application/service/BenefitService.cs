using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;

namespace PensionSystem.Application.service;

public class BenefitService : IBenefitService
{
    private readonly IUnitofWork _uow;
    public BenefitService(IUnitofWork uow) => _uow = uow;

    public async Task<Benefit> CalculateBenefitAsync(Guid memberId)
    {
       
        var total = await _uow.contributionRepo.SumContributions(memberId, null);
        bool eligible = total >= 100000;

        var benefit = new Benefit
        {
            MemberId = memberId,
            TotalContribution = total,
            Amount = eligible ? total * 0.1m : 0, // 10% benefit rate
            EligibilityStatus = eligible,
            CalculationDate = DateTime.UtcNow
        };

        return benefit;

      
    }
}
