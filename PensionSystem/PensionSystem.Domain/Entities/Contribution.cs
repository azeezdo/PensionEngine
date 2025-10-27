using PensionSystem.Domain.Enums;
namespace PensionSystem.Domain.Entities;

public class Contribution : BaseEntity
{
    public Guid MemberId { get; private set; }
    public ContributionType ContributionType { get; private set; }
    public decimal Amount { get; private set; }
    public DateTime ContributionDate { get; private set; }
    public string ReferenceNumber { get; private set; } = null!;

    protected Contribution() { }

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