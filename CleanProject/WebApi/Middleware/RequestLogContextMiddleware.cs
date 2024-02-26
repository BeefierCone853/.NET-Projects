using Serilog.Context;

namespace WebApi.Middleware;

/// <summary>
/// Middleware for logging requests.
/// </summary>
/// <param name="next">Request handler delegate.</param>
public class RequestLogContextMiddleware(RequestDelegate next)
{
    /// <summary>
    /// Adds unique identifier for the request in trace logs.
    /// </summary>
    /// <param name="context">Contains HTTP-specific information about the HTTP request.</param>
    /// <returns>Result of the request handler delegate.</returns>
    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", context.TraceIdentifier))
        {
            return next(context);
        }
    }
}