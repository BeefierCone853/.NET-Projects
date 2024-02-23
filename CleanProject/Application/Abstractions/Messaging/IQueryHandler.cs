using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

/// <summary>
/// Defines a query handler with a response.
/// </summary>
/// <typeparam name="TQuery">Incoming query of <see cref="IQuery{TResponse}"/> type.</typeparam>
/// <typeparam name="TResponse">Response from the handler.</typeparam>
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;