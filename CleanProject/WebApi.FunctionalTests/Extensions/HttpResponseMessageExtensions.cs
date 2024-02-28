using System.Net.Http.Json;
using Domain.Shared;
using WebApi.FunctionalTests.Contracts;

namespace WebApi.FunctionalTests.Extensions;

internal static class HttpResponseMessageExtensions
{
    internal static async Task<CustomProblemDetails> GetProblemDetails(
        this HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException("Successful response");
        }

        var problemDetails = await response
            .Content
            .ReadFromJsonAsync<CustomProblemDetails>();
        Ensure.NotNull(problemDetails);
        return problemDetails;
    }
}