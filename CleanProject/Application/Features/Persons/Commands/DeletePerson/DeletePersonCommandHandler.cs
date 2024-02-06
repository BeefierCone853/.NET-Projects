using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Commands.DeletePerson;

public class DeletePersonCommandHandler(
    IPersonRepository personRepository) : IRequestHandler<DeletePersonCommand, Unit>
{
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await personRepository.Get(request.Id);
        await personRepository.Delete(person);
        return Unit.Value;
    }
}