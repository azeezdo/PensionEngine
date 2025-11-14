using Microsoft.EntityFrameworkCore;
using PensionSystem.Domain.Entities;
using PensionSystem.Domain.Enums;
using PensionSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionSystem.Application.service
{
    public class MonthlyContributionValidator
    {
        private readonly PensionDbContext _context;

        public MonthlyContributionValidator(PensionDbContext context)
        {
            _context = context;
        }

        public async Task ValidateAsync(Contribution contribution)
        {
            if (contribution.ContributionType != ContributionType.Monthly)
                return; // Only validate for monthly type

            bool alreadyExists = await _context.Contributions.AnyAsync(c =>
                c.MemberId == contribution.MemberId &&
                c.ContributionType == ContributionType.Monthly &&
                c.ContributionDate.Month == contribution.ContributionDate.Month &&
                c.ContributionDate.Year == contribution.ContributionDate.Year);

            if (alreadyExists)
                throw new InvalidOperationException("A monthly contribution already exists for this member in the same month.");
        }
    }

}
