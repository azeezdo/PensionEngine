using FluentValidation;
using PensionSystem.Application.DTOs;
using PensionSystem.Domain.Models;

namespace PensionSystem.Application.Validators;

public class MemberDtoValidator : AbstractValidator<CreateMemberDto>
{
    public MemberDtoValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.DateOfBirth).NotEmpty().Must(BeBetween18And70).WithMessage("Member must be between 18 and 70 years of age.");
        RuleFor(x => x.PhoneNumber).Matches(@"^\+?\d{7,15}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber));
    }

    private bool BeBetween18And70(DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var age = today.Year - dob.Year - (today.DayOfYear < dob.DayOfYear ? 1 : 0);
        return age >= 18 && age <= 70;
    }
}