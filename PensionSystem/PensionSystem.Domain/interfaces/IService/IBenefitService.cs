using PensionSystem.Domain.Entities;

namespace PensionSystem.Domain.interfaces.IService;

public interface IBenefitService
{
    Task<Benefit> CalculateBenefitAsync(Guid memberId);
}