using System.Diagnostics.CodeAnalysis;

namespace Domain.Shared;

/// <summary>
/// Represents a standard result.
/// </summary>
public class Result
{
    /// <summary>
    /// Instantiates the result.
    /// </summary>
    /// <param name="isSuccess">Indicates if an operation was successful.</param>
    /// <param name="error">The error of an operation.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if an error was not specified with an unsuccessful operation.
    /// Thrown if an error was specified with a successful operation.
    /// </exception>
    protected internal Result(bool isSuccess, Error error)
    {
        if (isSuccess && error != Error.None ||
            !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid error", nameof(error));
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    /// <summary>
    /// Indicates if an operation was successful.
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    /// Indicates if an operation was unsuccessful.
    /// </summary>
    public bool IsFailure => !IsSuccess;

    /// <summary>
    /// The error of an operation.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Creates a result indicating an operation was successful and had no errors.
    /// </summary>
    /// <returns>A successful result with no errors.</returns>
    public static Result Success() => new(true, Error.None);

    /// <summary>
    /// Creates a result indicating an operation was successful and had no errors.
    /// </summary>
    /// <param name="value">The result of an operation.</param>
    /// <typeparam name="TValue">Type of the result.</typeparam>
    /// <returns>A successful result with a value from an operation.</returns>
    public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    /// <summary>
    /// Creates a result indicating an error has occured.
    /// </summary>
    /// <remarks>Check <see cref="ErrorType"/> for possible errors.</remarks>
    /// <param name="error">The error of an operation.</param>
    /// <returns>A failure result with an error.</returns>
    public static Result Failure(Error error) => new(false, error);

    /// <summary>
    /// Creates a result indicating an error has occured.
    /// </summary>
    /// <remarks>Check <see cref="ErrorType"/> for possible errors.</remarks>
    /// <param name="error">The error of an operation.</param>
    /// <typeparam name="TValue">The type of the result.</typeparam>
    /// <returns>A failure result with an error. The result is of a specified type.</returns>
    public static Result<TValue> Failure<TValue>(Error error) => new(default, false, error);
}

/// <summary>
/// Represents a standard result with a value of a certain type.
/// </summary>
/// <typeparam name="TValue">The type of the result.</typeparam>
public class Result<TValue> : Result
{
    /// <summary>
    /// Value of the result.
    /// </summary>
    private readonly TValue? _value;

    /// <summary>
    /// Instantiates the result with a value.
    /// </summary>
    /// <param name="value">The result of an operation.</param>
    /// <param name="isSuccess">Indicates if an operation was successful.</param>
    /// <param name="error">The error of an operation.</param>
    protected internal Result(TValue? value, bool isSuccess, Error error) : base(isSuccess, error)
    {
        _value = value;
    }

    /// <summary>
    /// Checks if operation was successful and returns a value.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if value couldn't be accessed.</exception>
    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    /// <summary>
    /// Converts a value into the result with the value of that type.
    /// </summary>
    /// <param name="value">The result of an operation.</param>
    /// <returns>Successful result with the value or a failure result if the value is null.</returns>
    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}