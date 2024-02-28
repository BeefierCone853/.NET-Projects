using Domain.Shared;

namespace WebApi.Infrastructure;

/// <summary>
/// Represents additional custom <see cref="Result"/>s.
/// </summary>
public static class CustomResults
{
    /// <summary>
    /// Creates a detailed problem response. 
    /// </summary>
    /// <param name="result">Result of on a operation.</param>
    /// <returns>Result with problem details.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the result of the operation is successful and this method is invoked.
    /// </exception>
    public static IResult Problem(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: GetStatusCode(result.Error.Type),
            title: GetTitle(result.Error),
            detail: GetDetail(result.Error),
            type: GetType(result.Error.Type),
            extensions: GetErrors(result));

        static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        static string GetTitle(Error error) =>
            error.Type switch
            {
                ErrorType.Validation => error.Code,
                ErrorType.NotFound => error.Code,
                ErrorType.Problem => error.Code,
                ErrorType.Conflict => error.Code,
                _ => "Internal Server Error"
            };

        static string GetDetail(Error error) =>
            error.Type switch
            {
                ErrorType.Validation => error.Description,
                ErrorType.NotFound => error.Description,
                ErrorType.Problem => error.Description,
                ErrorType.Conflict => error.Description,
                _ => "An unexpected error occured"
            };

        static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                ErrorType.Problem => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.Conflict => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.8",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1"
            };
        
        static Dictionary<string, object?>? GetErrors(Result result)
        {
            if (result.Error is not ValidationError validationError)
            {
                return null;
            }

            return new Dictionary<string, object?>
            {
                { "errors", validationError.Errors }
            };
        }
    }
}