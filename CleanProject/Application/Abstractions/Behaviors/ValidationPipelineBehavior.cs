using System.Reflection;
using Application.Abstractions.Messaging;
using Domain.Shared;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using ValidationError = Domain.Shared.ValidationError;

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
    /// <param name="request">Incoming request to validate.</param>
    /// <param name="next">Request handler delegate.</param>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>
    /// A failure result with validation errors if there are any.
    /// If there are no validation errors, it returns the result of the request handler delegate.
    /// </returns>
    /// <exception cref="ValidationException">
    /// Thrown if the response object is not a <see cref="Result{TValue}"/> or <see cref="Result"/> but there are validation errors.
    /// </exception>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        ValidationFailure[] validationFailures = await ValidateAsync(request);
        if (validationFailures.Length == 0)
        {
            return await next();
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            Type resultType = typeof(TResponse).GetGenericArguments()[0];
            MethodInfo? failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));
            if (failureMethod is not null)
            {
                return (TResponse)failureMethod.Invoke(
                    null,
                    [CreateValidationError(validationFailures)])!;
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
        }

        throw new ValidationException(validationFailures);
    }

    /// <summary>
    /// Runs validators and checks for validation failures.
    /// </summary>
    /// <param name="request">Incoming request to validate.</param>
    /// <returns>Array of validation failures.</returns>
    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);
        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));
        ValidationFailure[] validationFailures = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .ToArray();
        return validationFailures;
    }

    /// <summary>
    /// Creates an error which encapsulates multiple validation errors.
    /// </summary>
    /// <param name="validationFailures">Array of validation failures.</param>
    /// <returns>Object containing validation errors.</returns>
    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
        new(validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage)).ToArray());
}