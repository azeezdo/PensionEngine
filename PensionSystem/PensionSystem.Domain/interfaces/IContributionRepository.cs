
using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
namespace PensionSystem.Domain.interfaces;

public interface IContributionRepository: IGenericRepository<Contribution>
{
    Task<decimal> SumContributions(Guid memberId, ContributionType? type, CancellationToken ct = default);

    Task<bool> ExistsMonthlyContributionForMemberInMonth(Guid memberId, DateTime dateInMonth,
        CancellationToken ct = default);

    Task<IEnumerable<Contribution>> GetByMemberAsync(Guid memberId, CancellationToken ct = default);

    Task<bool> AddContributionAsync(Contribution contribution);
    Task<decimal> GetTotalContributionByTypeAsync(Guid memberId, ContributionType type);
    Task<IEnumerable<Contribution>> GetMemberContributionsAsync(Guid memberId);
   
}