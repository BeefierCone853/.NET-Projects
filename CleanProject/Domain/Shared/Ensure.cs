using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Shared;

/// <summary>
/// Guard clause class.
/// </summary>
public static class Ensure
{
    /// <summary>
    /// Ensures a value is not null.
    /// </summary>
    /// <param name="value">Value of the object.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown when parameter is null.</exception>
    public static void NotNull(
        [NotNull] object? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Ensures a value is not null or empty.
    /// </summary>
    /// <param name="value">Value of the object.</param>
    /// <param name="paramName">Name of the parameter.</param>
    /// <exception cref="ArgumentNullException">Thrown when parameter is null or empty.</exception>
    public static void NotNullOrEmpty(
        [NotNull] string? value,
        [CallerArgumentExpression("value")] string? paramName = default)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(paramName);
        }
    }
}