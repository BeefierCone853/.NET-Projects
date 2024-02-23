using Domain.Shared;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Application.Abstractions.Behaviors;

/// <summary>
/// Pipeline for logging requests.
/// </summary>
/// <param name="logger">Logger instance.</param>
/// <typeparam name="TRequest">Incoming request.</typeparam>
/// <typeparam name="TResponse">Response for the request.</typeparam>
internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
    /// <summary>
    /// Logs information about the request.
    /// </summary>
    /// <param name="request">Incoming request.</param>
    /// <param name="next">Request handler delegate.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Result of the request handler delegate.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        logger.LogInformation("Processing request {RequestName}", requestName);
        TResponse result = await next();
        if (result.IsSuccess)
        {
            logger.LogInformation("Completed request {RequestName}", requestName);
        }
        else
        {
            using (LogContext.PushProperty("Error", result.Error, true))
            {
                logger.LogError(
                    "Completed request {RequestName} with error",
                    requestName);
            }
        }

        return result;
    }
}