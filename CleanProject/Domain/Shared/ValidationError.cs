namespace Domain.Shared;

/// <summary>
/// Represents a validation error.
/// </summary>
/// <param name="Errors">Array of errors.</param>
public sealed record ValidationError(Error[] Errors) : Error(
    "Validation.General",
    "One or more validation errors occured",
    ErrorType.Validation)
{
    /// <summary>
    /// Creates a validation error from a <see cref="Result"/> object.
    /// </summary>
    /// <param name="results">Results from an operation.</param>
    /// <returns>Object containing validation errors.</returns>
    public static ValidationError FromResults(IEnumerable<Result> results) =>
        new(results.Where(r => r.IsFailure).Select(r => r.Error).ToArray());
}