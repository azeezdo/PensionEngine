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
        var total = await _uow.Contributions.SumContributions(memberId, null);
        var amount = total * 0.6m;
        var eligible = total > 0;
        var benefit = new Benefit(memberId, "Basic", DateTime.UtcNow, eligible, amount);
        return benefit;
    }
}
