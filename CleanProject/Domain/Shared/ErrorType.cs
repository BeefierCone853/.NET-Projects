namespace Domain.Shared;

/// <summary>
/// Defines an enum for error types.
/// </summary>
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    Problem = 2,
    NotFound = 3,
    Conflict = 4
}