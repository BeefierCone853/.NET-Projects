namespace Application.Exceptions;

/// <summary>
/// Represents a custom exception for validation errors.
/// </summary>
/// <param name="errors">Collection with validation errors.</param>
public sealed class CustomValidationException(IReadOnlyCollection<ValidationError> errors)
    : Exception("Validation failed")
{
    /// <summary>
    /// Collection with validation errors.
    /// </summary>
    public IReadOnlyCollection<ValidationError> Errors { get; } = errors;
}

/// <summary>
/// Represents a validation error with the name of the property and the error message.
/// </summary>
/// <param name="PropertyName">Name of the property which failed validation.</param>
/// <param name="ErrorMessage">Description of the validation error.</param>
public sealed record ValidationError(string PropertyName, string ErrorMessage);