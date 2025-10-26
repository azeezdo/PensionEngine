using FluentValidation;
using PensionSystem.Application.DTOs;

namespace PensionSystem.Application.Validators;

public class ContributionDtoValidator : AbstractValidator<CreateContributionDto>
{
    public ContributionDtoValidator()
    {
        RuleFor(x => x.MemberId).NotEmpty();
        RuleFor(x => x.Amount).GreaterThan(0);
        RuleFor(x => x.ReferenceNumber).NotEmpty().Matches(@"^[A-Z0-9\-]{6,}$").WithMessage("Reference must be alphanumeric and at least 6 chars");
        RuleFor(x => x.ContributionDate).NotEmpty();
        // monthly-vs-voluntary date rule is implemented as business rule (DB check)
    }
}