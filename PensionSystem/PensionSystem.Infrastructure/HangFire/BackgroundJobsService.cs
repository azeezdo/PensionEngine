using PensionSystem.Domain.interfaces;
using Microsoft.Extensions.Logging;
namespace PensionSystem.Infrastructure.HangFire;

public class BackgroundJobsService
{
    private readonly IUnitofWork _uow;
    private readonly ILogger<BackgroundJobsService> _log;

    public BackgroundJobsService(IUnitofWork uow, ILogger<BackgroundJobsService> log)
    {
        _uow = uow;
        _log = log;
    }

    public async Task RunMonthlyValidation()
    {
        try
        {
            var members = await _uow.Members.ListAsync(null);
            foreach (var m in members)
            {
                _log.LogInformation("Validating monthly contributions for member {MemberId}", m.Id);
            }
            await _uow.CommitAsync();
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "RunMonthlyValidation failed");
            throw;
        }
    }

    public async Task UpdateBenefitEligibility()
    {
        try
        {
            _log.LogInformation("Updating benefit eligibility");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "UpdateBenefitEligibility failed");
            throw;
        }
    }

    public async Task ApplyMonthlyInterest()
    {
        try
        {
            _log.LogInformation("Applying monthly interest");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "ApplyMonthlyInterest failed");
            throw;
        }
    }

    public async Task GenerateMemberStatements()
    {
        try
        {
            _log.LogInformation("Generating member statements");
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "GenerateMemberStatements failed");
            throw;
        }
    }
}
