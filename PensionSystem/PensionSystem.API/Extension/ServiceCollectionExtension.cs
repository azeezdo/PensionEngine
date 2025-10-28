using Microsoft.EntityFrameworkCore;
using PensionSystem.Application.service;
using PensionSystem.Domain.interfaces;
using PensionSystem.Domain.interfaces.IService;
using PensionSystem.Infrastructure.Data;
using PensionSystem.Infrastructure.HangFire;
using PensionSystem.Infrastructure.Repositories;
using PensionSystem.Infrastructure.Unitofwork;

namespace PensionSystem.API.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPensionServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitofWork, UnitOfWork>();

        services.AddScoped<IMemberService, MemberService>();
        services.AddScoped<IContributionService, ContributionService>();
        services.AddScoped<IBenefitService, BenefitService>();
        services.AddScoped<IEmployerService, EmployerService>();

        services.AddScoped<IEmployerRepository, EmployerRepository>();
        services.AddScoped<IMemberRepository, MemberRepository>();
        services.AddScoped<IContributionRepository, ContributionRepository>();
        services.AddScoped<IBenefitRepository, BenefitRepository>();
        services.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();

        services.AddScoped<MonthlyContributionRule>();
        services.AddScoped<BackgroundJobsService>();

        return services;
    }

    
}
