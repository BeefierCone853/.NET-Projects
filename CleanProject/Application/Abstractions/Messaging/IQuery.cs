using Domain.Shared;
using MediatR;

namespace Application.Abstractions.Messaging;

/// <summary>
/// Defines a query with a response.
/// </summary>
/// <typeparam name="TResponse">Response from the operation.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;