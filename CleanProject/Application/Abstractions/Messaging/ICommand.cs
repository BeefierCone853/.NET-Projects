using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

public interface ICommand : IRequest<Result>, ICommandBase;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, ICommandBase;

public interface ICommandBase;