using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

/// <summary>
/// Defines a command handler without a response.
/// </summary>
/// <typeparam name="TCommand">Incoming command of <see cref="ICommandBase"/> type.</typeparam>
public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
/// Defines a command handler with a response.
/// </summary>
/// <typeparam name="TCommand">Incoming command of <see cref="ICommandBase"/> type.</typeparam>
/// <typeparam name="TResponse">Response from the handler.</typeparam>
public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;