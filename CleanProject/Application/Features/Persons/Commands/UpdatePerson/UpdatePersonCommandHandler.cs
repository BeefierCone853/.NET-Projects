using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Commands.UpdatePerson;

public class UpdatePersonCommandHandler(
    IPersonRepository personRepository,
    IMapperBase mapper) : IRequestHandler<UpdatePersonCommand, Unit>
{
    public async Task<Unit> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await personRepository.Get(request.PersonDto.Id);
        mapper.Map(request.PersonDto, person);
        await personRepository.Update(person);
        return Unit.Value;
    }
}