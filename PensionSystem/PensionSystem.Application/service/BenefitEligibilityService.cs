using Microsoft.EntityFrameworkCore;
using PensionSystem.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PensionSystem.Application.service
{
    public class BenefitEligibilityService
    {
        private readonly PensionDbContext _context;

        public BenefitEligibilityService(PensionDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsEligibleAsync(Guid memberId)
        {
            var contributions = await _context.Contributions
                .Where(c => c.MemberId == memberId)
                .OrderBy(c => c.ContributionDate)
                .ToListAsync();

            if (contributions.Count < 12)
                return false;

            var first = contributions.First().ContributionDate;
            var last = contributions.Last().ContributionDate;
            int months = ((last.Year - first.Year) * 12) + last.Month - first.Month + 1;

            return months >= 12;
        }

    }
}
