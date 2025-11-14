using Microsoft.EntityFrameworkCore;
using PensionSystem.Application.service;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionSystem.Tests.UnitTest
{
    public class BenefitEligibilityTest
    {
        public class BenefitEligibilityServiceTests
        {
            private PensionDbContext GetDbContext()
            {
                var options = new DbContextOptionsBuilder<PensionDbContext>()
                    .UseSqlServer(Guid.NewGuid().ToString())
                    .Options;

                return new PensionDbContext(options);
            }
            private readonly IBenefitService _benefit;
            public BenefitEligibilityServiceTests(IBenefitService ben)
            {
                _benefit = ben;
            }


            [Test]
            public async Task Should_Not_Be_Eligible_If_Less_Than_12_Months()
            {
                var context = GetDbContext();
                var service = new BenefitEligibilityService(context);

                for (int i = 0; i < 6; i++)
                {
                    context.Contributions.Add(new Contribution
                    {
                        MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
                        ContributionType = ContributionType.Monthly,
                        Amount = 1000,
                        ContributionDate = DateTime.UtcNow.AddMonths(-i)
                    });
                }

                await context.SaveChangesAsync();
                var eligible = await service.IsEligibleAsync(Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"));

                Assert.False(eligible);
            }

            [Test]
            public async Task Should_Be_Eligible_If_12_Months_Contributions()
            {
                var context = GetDbContext();
                var service = new BenefitEligibilityService(context);
                

                for (int i = 0; i < 12; i++)
                {
                    context.Contributions.Add(new Contribution
                    {
                        MemberId = Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"),
                        ContributionType = ContributionType.Monthly,
                        Amount = 1000,
                        ContributionDate = DateTime.UtcNow.AddMonths(-i)
                    });
                }

                await context.SaveChangesAsync();

                var eligible = await service.IsEligibleAsync(Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"));
                Assert.True(eligible);

                var benefit = await _benefit.CalculateBenefitAsync(Guid.Parse("F85E5FDB-6D01-47A6-957C-42DA9C737D20"));
                Assert.AreEqual(12000, benefit.TotalContribution);
                Assert.AreEqual(1200, benefit.Amount); // 10%
            }
        }

    }
}
