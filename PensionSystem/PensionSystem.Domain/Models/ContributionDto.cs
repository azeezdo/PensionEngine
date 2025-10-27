using PensionSystem.Domain.Enums;

namespace PensionSystem.Application.DTOs;

public record ContributionDto(Guid Id, Guid MemberId, ContributionType ContributionType, decimal Amount, DateTime ContributionDate, string ReferenceNumber);

public record CreateContributionDto(Guid MemberId, ContributionType ContributionType, decimal Amount, DateTime ContributionDate, string ReferenceNumber);