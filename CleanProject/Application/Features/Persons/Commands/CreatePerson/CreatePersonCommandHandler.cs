using Application.Abstractions;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Features.Persons.Commands.CreatePerson;

internal sealed class CreatePersonCommandHandler(
    IPersonRepository personRepository,
    IMapper mapper) : ICommandHandler<CreatePersonCommand>
{
    public async Task<Result> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreatePersonCommandValidator();
        var validationResult = await validator.ValidateAsync(request.CreatePersonDto, cancellationToken);
        if (!validationResult.IsValid) throw new ValidationException(validationResult);
        var person = mapper.Map<Person>(request.CreatePersonDto);
        await personRepository.Add(person);
        return Result.Success(person.Id);
    }
}