using Microsoft.EntityFrameworkCore;
using PensionSystem.Application.service;
using PensionSystem.Domain.interfaces;
using PensionSystem.Infrastructure.Data;
using PensionSystem.Infrastructure.Unitofwork;
using PensionSystem.Infrastructure.HangFire;
using PensionSystem.Domain.interfaces.IService;

namespace PensionSystem.API.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPensionServices(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<PensionDbContext>(opt => opt.UseSqlServer(connectionString));

        services.AddScoped<IUnitofWork, UnitOfWork>();

        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IContributionService, ContributionService>();
        services.AddScoped<IBenefitService, BenefitService>();
        services.AddScoped<IEmployerService, EmployerService>();

        services.AddScoped<MonthlyContributionRule>();
        services.AddScoped<BackgroundJobsService>();

        return services;
    }
}
