using System.Text.Json;

namespace PensionSystem.API.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _log;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> log)
    {
        _next = next;
        _log = log;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _log.LogError(ex, "Unhandled exception");
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = ex is InvalidOperationException ? 400 : 500;
            var res = JsonSerializer.Serialize(new { error = ex.Message });
            await httpContext.Response.WriteAsync(res);
        }
    }
}