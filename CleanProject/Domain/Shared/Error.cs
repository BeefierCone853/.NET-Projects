namespace Domain.Shared;

/// <summary>
/// Represents a standard error.
/// </summary>
public sealed record Error
{
    /// <summary>
    /// Creates a failure error with an empty code and description strings.
    /// The error type is <see cref="ErrorType.Failure"/>.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty, ErrorType.Failure);

    /// <summary>
    /// Creates a failure error for a null value. The error type is <see cref="ErrorType.Failure"/>.
    /// </summary>
    public static readonly Error NullValue = new(
        "Error.NullValue",
        "The specified result value is null.",
        ErrorType.Failure);
    
    /// <summary>
    /// Instantiates an error record. 
    /// </summary>
    /// <param name="code">Code for the error.</param>
    /// <param name="description">Description of the error.</param>
    /// <param name="errorType">Type of the error.</param>
    private Error(string code, string description, ErrorType errorType)
    {
        Code = code;
        Description = description;
        Type = errorType;
    }

    /// <summary>
    /// Code for the error.
    /// </summary>
    public string Code { get; }
    
    /// <summary>
    /// Description of the error.
    /// </summary>
    public string Description { get; }
    
    /// <summary>
    /// Type of the error.
    /// </summary>
    public ErrorType Type { get; }

    /// <summary>
    /// Creates an error indicating a failure has occured.
    /// </summary>
    /// <param name="code">Code for the error.</param>
    /// <param name="description">Description of the error.</param>
    /// <returns>A failure error with <see cref="ErrorType.Failure"/> type.</returns>
    public static Error Failure(string code, string description) =>
        new(code, description, ErrorType.Failure);

    /// <summary>
    /// Creates an error indicating a validation error has occured.
    /// </summary>
    /// <param name="code">Code for the error.</param>
    /// <param name="description">Description of the error.</param>
    /// <returns>A validation error with <see cref="ErrorType.Validation"/> type.</returns>
    public static Error Validation(string code, string description) =>
        new(code, description, ErrorType.Validation);

    /// <summary>
    /// Creates an error indicating a not found error has occured.
    /// </summary>
    /// <param name="code">Code for the error.</param>
    /// <param name="description">Description of the error.</param>
    /// <returns>A not found error with <see cref="ErrorType.NotFound"/> type.</returns>
    public static Error NotFound(string code, string description) =>
        new(code, description, ErrorType.NotFound);

    /// <summary>
    /// Creates an error indicating a conflict error has occured.
    /// </summary>
    /// <param name="code">Code for the error.</param>
    /// <param name="description">Description of the error.</param>
    /// <returns>A conflict error with <see cref="ErrorType.Conflict"/> type.</returns>
    public static Error Conflict(string code, string description) =>
        new(code, description, ErrorType.Conflict);
}

/// <summary>
/// Defines an enum for error types.
/// </summary>
public enum ErrorType
{
    Failure = 0,
    Validation = 1,
    NotFound = 2,
    Conflict = 3
}