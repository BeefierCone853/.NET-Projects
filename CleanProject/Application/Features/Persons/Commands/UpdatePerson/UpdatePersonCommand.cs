using Application.DTOs.Persons;
using MediatR;

namespace Application.Features.Persons.Commands.UpdatePerson;

public sealed record UpdatePersonCommand(UpdatePersonDto PersonDto) : IRequest<Unit>;