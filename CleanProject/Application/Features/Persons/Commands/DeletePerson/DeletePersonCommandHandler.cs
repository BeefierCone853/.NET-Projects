using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Commands.DeletePerson;

internal sealed class DeletePersonCommandHandler(
    IPersonRepository personRepository) : IRequestHandler<DeletePersonCommand, Unit>
{
    public async Task<Unit> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await personRepository.Get(request.Id);
        if (person != null) await personRepository.Delete(person);
        return Unit.Value;
    }
}