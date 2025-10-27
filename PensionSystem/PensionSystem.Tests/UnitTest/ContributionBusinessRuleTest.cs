using NSubstitute;
using PensionSystem.Application.DTOs;
using PensionSystem.Application.service;
using PensionSystem.Application.Validators;
using PensionSystem.Domain.interfaces;


namespace PensionSystem.Tests.UnitTest;


public class ContributionBusinessRuleTests
{
    [Test]
    public async Task MonthlyRule_Allows_When_NoExisting()
    {
        var uow = Substitute.For<IUnitofWork>();
        uow.Contributions.ExistsMonthlyContributionForMemberInMonth(Arg.Any<Guid>(), Arg.Any<DateTime>()).Returns(false);
        var rule = new MonthlyContributionRule(uow);
        var ok = await rule.CanAddMonthlyContributionAsync(Guid.NewGuid(), DateTime.UtcNow);
        Assert.IsTrue(ok);
    }
}