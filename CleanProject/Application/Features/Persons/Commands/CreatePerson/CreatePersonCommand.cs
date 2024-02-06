using Application.DTOs;
using Application.DTOs.Persons;
using MediatR;

namespace Application.Features.Persons.Commands.CreatePerson;

public sealed record CreatePersonCommand(CreatePersonDto PersonDto) : IRequest<int>;