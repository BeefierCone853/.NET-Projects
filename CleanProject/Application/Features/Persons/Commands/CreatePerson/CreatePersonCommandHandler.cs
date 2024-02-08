using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Commands.CreatePerson;

internal sealed class CreatePersonCommandHandler(
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<CreatePersonCommand, int>
{
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePersonCommandValidator();
        var validationResult = await validator.ValidateAsync(request.CreatePersonDto, cancellationToken);
        if (!validationResult.IsValid) throw new ValidationException(validationResult);
        var person = mapper.Map<Person>(request.CreatePersonDto);
        person = await personRepository.Add(person);
        return person.Id;
    }
}