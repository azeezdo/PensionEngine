namespace PensionSystem.Domain.Entities;

public class Benefit : BaseEntity
{
    public Guid MemberId { get; private set; }
    public string BenefitType { get; private set; } = null!;
    public DateTime CalculationDate { get; private set; }
    public bool EligibilityStatus { get; private set; }
    public decimal Amount { get; private set; }

    protected Benefit() { }

    public Benefit(Guid memberId, string benefitType, DateTime calculationDate, bool eligibilityStatus, decimal amount)
    {
        MemberId = memberId;
        BenefitType = benefitType;
        CalculationDate = calculationDate;
        EligibilityStatus = eligibilityStatus;
        Amount = amount;
    }
}