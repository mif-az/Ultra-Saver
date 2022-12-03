using System.Diagnostics;

namespace Ultra_Saver;

public class LoggingInterceptor
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggingInterceptor> _logger;

    public LoggingInterceptor(RequestDelegate next, ILogger<LoggingInterceptor> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // This will catch all exceptions and log them
        // If no exception thrown - the request statistics will be logged

        try
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            await _next(context);
            s.Stop();

            _logger.LogInformation($"Request to {context.GetEndpoint()} took {s.ElapsedMilliseconds} ms to complete.");

        }
        catch (Exception e)
        {
            _logger.LogError(e, "This exception was caught in the logging interceptor!");
            throw;
        }

    }
}