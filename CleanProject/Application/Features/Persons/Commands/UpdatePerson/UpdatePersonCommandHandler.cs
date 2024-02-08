using Application.Exceptions;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Commands.UpdatePerson;

internal sealed class UpdatePersonCommandHandler(
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<UpdatePersonCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdatePersonCommandValidator(personRepository);
        var validationResult = await validator.ValidateAsync(request.UpdatePersonDto, cancellationToken);
        if (!validationResult.IsValid) throw new ValidationException(validationResult);
        var person = await personRepository.Get(request.UpdatePersonDto.Id);
        mapper.Map(request.UpdatePersonDto, person);
        if (person != null) await personRepository.Update(person);
        return Unit.Value;
    }
}