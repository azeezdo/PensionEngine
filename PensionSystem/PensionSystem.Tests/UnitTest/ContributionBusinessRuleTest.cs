using Microsoft.EntityFrameworkCore;
using NSubstitute;
using PensionSystem.Application.DTOs;
using PensionSystem.Application.service;
using PensionSystem.Application.Validators;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Infrastructure.Data;


namespace PensionSystem.Tests.UnitTest;


public class ContributionBusinessRuleTests
{
    private PensionDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<PensionDbContext>()
            .UseSqlServer(Guid.NewGuid().ToString())
            .Options;

        return new PensionDbContext(options);
    }
    private readonly IContributionService _service;
    public ContributionBusinessRuleTests(IContributionService service)
    {
        _service = service;
    }

    [Test]
    public async Task MonthlyRule_Allows_When_NoExisting()
    {
        var uow = Substitute.For<IUnitofWork>();
        uow.contributionRepo.ExistsMonthlyContributionForMemberInMonth(Arg.Any<Guid>(), Arg.Any<DateTime>()).Returns(false);
        var rule = new MonthlyContributionRule(uow);
        var ok = await rule.CanAddMonthlyContributionAsync(Guid.NewGuid(), DateTime.UtcNow);
        Assert.IsTrue(ok);
    }

    [Test]
    public async Task Should_Allow_First_Monthly_Contribution()
    {
        var context = GetDbContext();
        var validator = new MonthlyContributionValidator(context);

        var contribution = new Contribution
        {
            MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
            ContributionType = ContributionType.Monthly,
            Amount = 1000,
            ContributionDate = new DateTime(2025, 1, 10)
        };

        Assert.DoesNotThrowAsync(async () => await validator.ValidateAsync(contribution));
    }

    [Test]
    public async Task Should_Reject_Duplicate_Monthly_Contribution_Same_Month()
    {
        var context = GetDbContext();
        var validator = new MonthlyContributionValidator(context);

        var existing = new Contribution
        {
            MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
            ContributionType = ContributionType.Monthly,
            Amount = 1000,
            ContributionDate = new DateTime(2025, 1, 10)
        };
        context.Contributions.Add(existing);
        await context.SaveChangesAsync();

        var duplicate = new Contribution
        {
            MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
            ContributionType = ContributionType.Monthly,
            Amount = 1200,
            ContributionDate = new DateTime(2025, 1, 25)
        };

        Assert.ThrowsAsync<InvalidOperationException>(async () =>
            await validator.ValidateAsync(duplicate)
        );
    }


        [Test]
        public async Task Should_Reject_Negative_Contribution_Amount()
        {
            var context = GetDbContext();

            var invalid = new Contribution
            {
                MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
                ContributionType = ContributionType.Voluntary,
                Amount = -500,
                ContributionDate = DateTime.UtcNow
            };

            Assert.ThrowsAsync<InvalidOperationException>(
                async () => await _service.AddContributionAsync(invalid)
            );
        }

        [Test]
        public async Task Should_Allow_Multiple_Voluntary_Contributions()
        {
            var context = GetDbContext();

            for (int i = 0; i < 3; i++)
            {
                var contribution = new Contribution
                {
                    MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
                    ContributionType = ContributionType.Voluntary,
                    Amount = 500,
                    ContributionDate = DateTime.UtcNow.AddDays(i)
                };
                await _service.AddContributionAsync(contribution);
            }

            var count = await context.Contributions.CountAsync();
            Assert.AreEqual(3, count);
        }
 }
