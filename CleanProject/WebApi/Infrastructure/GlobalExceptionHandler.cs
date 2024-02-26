using Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Infrastructure;

/// <summary>
/// Handles uncaught exceptions throughout the application.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    /// <summary>
    /// Handles uncaught exceptions and creates a response containing details about the error.
    /// </summary>
    /// <param name="httpContext">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="exception">Unhandled exception.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Result of the operation.</returns>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var exceptionDetails = GetExceptionDetails(exception);
        var problemDetails = new ProblemDetails
        {
            Status = exceptionDetails.Status,
            Type = exceptionDetails.Type,
            Title = exceptionDetails.Title,
            Detail = exceptionDetails.Detail,
        };
        if (exceptionDetails.Errors is not null)
        {
            problemDetails.Extensions["errors"] = exceptionDetails.Errors;
        }

        httpContext.Response.StatusCode = exceptionDetails.Status;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    /// <summary>
    /// Creates a <see cref="ExceptionDetails"/> object based on the exception type.
    /// </summary>
    /// <param name="exception">Uncaught exception.</param>
    /// <returns>An <see cref="ExceptionDetails"/> object containing validation or server errors based on the exception type.</returns>
    private static ExceptionDetails GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            CustomValidationException validationException => new ExceptionDetails(
                StatusCodes.Status400BadRequest,
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                "ValidationError",
                "One or more validation errors has occured",
                validationException.Errors),
            _ => new ExceptionDetails(
                StatusCodes.Status500InternalServerError,
                "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                "Server error",
                "An unexpected error has occured",
                null)
        };
    }

    /// <summary>
    /// Represents the exception details that can contain multiple errors with their detailed information.
    /// </summary>
    /// <param name="Status">Status of the error.</param>
    /// <param name="Type">Type of the error.</param>
    /// <param name="Title">Title of the error.</param>
    /// <param name="Detail">Additional information of the error.</param>
    /// <param name="Errors">List of errors.</param>
    private record ExceptionDetails(
        int Status,
        string Type,
        string Title,
        string Detail,
        IEnumerable<object>? Errors);
}