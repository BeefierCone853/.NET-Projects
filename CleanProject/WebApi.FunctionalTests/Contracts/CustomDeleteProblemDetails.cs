using Domain.Shared;

namespace WebApi.FunctionalTests.Contracts;

internal sealed class CustomDeleteProblemDetails
{
    public string Code { get; set; }
    public string Description { get; set; }
    public ErrorType Type { get; set; }
}