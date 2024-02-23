using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

/// <summary>
/// Defines a command without a response. 
/// </summary>
public interface ICommand : IRequest<Result>, ICommandBase;

/// <summary>
/// Defines a command with a response.
/// </summary>
/// <typeparam name="TResponse">Response from the operation.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase;

/// <summary>
/// Base for all the commands.
/// </summary>
public interface ICommandBase;