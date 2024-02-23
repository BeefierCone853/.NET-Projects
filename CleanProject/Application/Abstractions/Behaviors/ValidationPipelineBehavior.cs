using System.Reflection;
using Application.Abstractions.Messaging;
using Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Abstractions.Behaviors;

/// <summary>
/// Pipeline which validates requests.
/// </summary>
/// <param name="validators">Collection with validators.</param>
/// <typeparam name="TRequest">Incoming request.</typeparam>
/// <typeparam name="TResponse">Response for the request.</typeparam>
internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
    /// <summary>
    /// Validates the incoming requests.
    /// </summary>
    /// <param name="request">Incoming requests.</param>
    /// <param name="next">Request handler delegate.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Result of the request handler delegate.</returns>
    /// <exception cref="CustomValidationException">Thrown if there are validation errors.</exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var validationFailures = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));
        var errors = validationFailures
            .Where(result => !result.IsValid)
            .SelectMany(result => result.Errors)
            .Select(failure => new ValidationError(
                failure.PropertyName,
                failure.ErrorMessage
            ))
            .ToArray();
        if (errors.Length != 0)
        {
            throw new CustomValidationException(errors);
        }

        var response = await next();
        return response;
    }
}