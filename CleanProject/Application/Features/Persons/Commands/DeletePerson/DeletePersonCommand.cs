using MediatR;

namespace Application.Features.Persons.Commands.DeletePerson;

public sealed record DeletePersonCommand(int Id) : IRequest<Unit>;