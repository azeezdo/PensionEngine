using PensionSystem.Domain.Enums;
namespace PensionSystem.Domain.Entities;

public class Contribution : BaseEntity
{
    public Guid MemberId { get; set; }
    public ContributionType ContributionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime ContributionDate { get; set; }
    public string ReferenceNumber { get; set; } = null!;

    public Contribution() { }

    public Contribution(Guid memberId, ContributionType contributionType, decimal amount, DateTime contributionDate, string referenceNumber)
    {
        MemberId = memberId;
        ContributionType = contributionType;
        Amount = amount;
        ContributionDate = contributionDate;
        ReferenceNumber = referenceNumber;
        CreatedAt = DateTime.UtcNow;
    }
}