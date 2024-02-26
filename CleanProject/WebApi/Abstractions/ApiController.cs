using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Abstractions;

/// <summary>
/// Base controller class with a request sender for the mediator pipeline.
/// </summary>
/// <param name="sender">Sends a request through the mediator pipeline.</param>
public abstract class ApiController(ISender sender) : ControllerBase
{
    /// <summary>
    /// Sends a request through the mediator pipeline.
    /// </summary>
    protected readonly ISender Sender = sender;
}