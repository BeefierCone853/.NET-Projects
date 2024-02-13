using Application.Abstractions.Messaging;
using Application.Exceptions;
using FluentValidation;
using MediatR;

namespace Application.Abstractions.Behaviors;

internal class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommandBase
{
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