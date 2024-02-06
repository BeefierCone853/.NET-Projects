using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Commands.CreatePerson;

internal sealed class CreatePersonCommandHandler(
    IPersonRepository personRepository,
    IMapperBase mapper) : IRequestHandler<CreatePersonCommand, int>
{
    public async Task<int> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePersonCommandValidator();
        var validationResult = await validator.ValidateAsync(request.PersonDto, cancellationToken);
        if (!validationResult.IsValid) throw new Exception();
        var person = mapper.Map<Person>(request.PersonDto);
        person = await personRepository.Add(person);
        return person.Id;
    }
}