using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PensionSystem.API.Extension;
using PensionSystem.API.Middleware;
using PensionSystem.Infrastructure.HangFire;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection") ??
           "Server=.;Database = PensionDb; Integrated Security = true; TrustServerCertificate = True;";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPensionServices(conn);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHangfire(cfg => cfg.UseSqlServerStorage(conn));
builder.Services.AddHangfireServer();

builder.Services.AddApiVersioning(options => {
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});


var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseHangfireDashboard("/hangfire");

app.MapControllers();

RecurringJob.AddOrUpdate<BackgroundJobsService>("monthly-validation", job => job.RunMonthlyValidation(), Cron.Monthly);
RecurringJob.AddOrUpdate<BackgroundJobsService>("benefit-update", job => job.UpdateBenefitEligibility(), Cron.Monthly);
RecurringJob.AddOrUpdate<BackgroundJobsService>("interest-apply", job => job.ApplyMonthlyInterest(), Cron.Monthly);
RecurringJob.AddOrUpdate<BackgroundJobsService>("generate-statements", job => job.GenerateMemberStatements(), Cron.Monthly);

app.Run();