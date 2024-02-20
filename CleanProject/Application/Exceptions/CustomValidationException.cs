namespace Application.Exceptions;

public sealed class CustomValidationException(IReadOnlyCollection<ValidationError> errors)
    : Exception("Validation failed")
{
    public IReadOnlyCollection<ValidationError> Errors { get; } = errors;
}

public sealed record ValidationError(string PropertyName, string ErrorMessage);