using Microsoft.EntityFrameworkCore;
using PensionSystem.Application.service;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;
using PensionSystem.Infrastructure.Unitofwork;

namespace PensionSystem.API.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPensionServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PensionDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IUnitofWork, UnitOfWork>();

        services.AddScoped<MemberService>();
        services.AddScoped<ContributionService>();
        services.AddScoped<BenefitService>();

        services.AddScoped<MonthlyContributionRule>();
        services.AddScoped<BackgroundJobsService>();

        return services;
    }
}
