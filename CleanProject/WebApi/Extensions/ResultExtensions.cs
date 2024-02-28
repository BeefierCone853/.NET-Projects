using Domain.Shared;

namespace WebApi.Extensions;

/// <summary>
/// Extensions for the <see cref="Result"/> class.
/// </summary>
public static class ResultExtensions
{
    /// <summary>
    /// Checks if the result of the operation was successful and executes the appropriate function.
    /// </summary>
    /// <param name="result">Represents the result of an operation.</param>
    /// <param name="onSuccess">Function to execute if the result is successful.</param>
    /// <param name="onFailure">Function to execute if the result is unsuccessful.</param>
    /// <typeparam name="TOut">HTTP response.</typeparam>
    /// <returns>An <see cref="IResult"/> based on the result of the operation.</returns>
    public static TOut Match<TOut>(
        this Result result,
        Func<TOut> onSuccess,
        Func<Result, TOut> onFailure)
        where TOut : IResult
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    /// <summary>
    /// Checks if the result of the operation was successful and executes the appropriate function.
    /// </summary>
    /// <param name="result">Represents the result of an operation.</param>
    /// <param name="onSuccess">Function to execute if the result is successful.</param>
    /// <param name="onFailure">Function to execute if the result is unsuccessful.</param>
    /// <typeparam name="TIn">Value from the operation.</typeparam>
    /// <typeparam name="TOut">HTTP response.</typeparam>
    /// <returns>An <see cref="IResult"/> based on the result of the operation.</returns>
    public static TOut Match<TIn, TOut>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
        where TOut : IResult
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}