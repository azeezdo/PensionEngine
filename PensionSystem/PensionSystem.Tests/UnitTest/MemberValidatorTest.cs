using PensionSystem.Application.DTOs;
using PensionSystem.Application.Validators;
using PensionSystem.Domain.Models;

namespace PensionSystem.Tests.UnitTest;

public class MemberValidatorTests
{
    [Test]
    public void MemberValidator_InvalidAge_ShouldFail()
    {
        var dto = new CreateMemberDto("First","Last", DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-17)), "test@example.com", null);
        var v = new MemberDtoValidator();
        var res = v.Validate(dto);
        Assert.IsFalse(res.IsValid);
    }
}