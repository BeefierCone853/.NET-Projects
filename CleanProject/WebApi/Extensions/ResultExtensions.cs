using Domain.Shared;

namespace WebApi.Extensions;

/// <summary>
/// Extensions for the <see cref="Result"/> class.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Creates a detailed problem response. 
    /// </summary>
    /// <param name="result">Result of on a operation.</param>
    /// <returns>Result with problem details.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the result of the operation is successful and this method is invoked.
    /// </exception>
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error.Type),
            type: GetType(result.Error.Type),
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error } }
            });
    }

    /// <summary>
    /// Gets a HTTP status code.
    /// </summary>
    /// <param name="errorType">Type of the error.</param>
    /// <returns>HTTP status code of the error based on the error type.</returns>
    private static int GetStatusCode(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

    /// <summary>
    /// Gets a title for the error.
    /// </summary>
    /// <param name="errorType">Type of the error.</param>
    /// <returns>Title of the error based on the error type.</returns>
    private static string GetTitle(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "Bad Request",
            ErrorType.NotFound => "Not Found",
            ErrorType.Conflict => "Conflict",
            _ => "Internal Server Error"
        };

    /// <summary>
    /// Gets a URL with detailed information.
    /// </summary>
    /// <param name="errorType">Type of the error.</param>
    /// <returns>URL with detailed information about the error based on its type.</returns>
    private static string GetType(ErrorType errorType) =>
        errorType switch
        {
            ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
            ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
            _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
        };
}