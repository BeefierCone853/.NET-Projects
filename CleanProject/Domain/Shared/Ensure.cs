using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Domain.Shared;

/// <summary>
/// Helper class for ensuring a state of the parameter.
/// </summary>
public static class Ensure
{
    /// <summary>
    /// Ensures a value of the parameter is not null.
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
}