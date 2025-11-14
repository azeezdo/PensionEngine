namespace PensionSystem.Domain.Entities;

public class Benefit : BaseEntity
{
    public Guid MemberId { get; set; }
    public string BenefitType { get; set; } = null!;
    public DateTime CalculationDate { get; set; }
    public bool EligibilityStatus { get; set; }
    public decimal Amount { get; set; }
    public decimal TotalContribution { get; set; }

    public Benefit() { }

    public Benefit(Guid memberId, string benefitType, DateTime calculationDate, bool eligibilityStatus, decimal amount, decimal totalContribution)
    {
        MemberId = memberId;
        BenefitType = benefitType;
        CalculationDate = calculationDate;
        EligibilityStatus = eligibilityStatus;
        Amount = amount;
        TotalContribution = totalContribution;
        CreatedAt = DateTime.UtcNow;
    }
}