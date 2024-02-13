using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Abstractions;

public abstract class ApiController(ISender sender) : ControllerBase
{
    protected readonly ISender Sender = sender;
}