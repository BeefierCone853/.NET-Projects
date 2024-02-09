using Application.Abstractions;
using Domain.Repositories;
using Domain.Shared;
using MediatR;

namespace Application.Features.Persons.Commands.DeletePerson;

internal sealed class DeletePersonCommandHandler(
    IPersonRepository personRepository) : ICommandHandler<DeletePersonCommand>
{
    public async Task<Result> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await personRepository.Get(request.Id);
        if (person == null) return Result.Failure(Error.NotFound);
        await personRepository.Delete(person);
        return Result.Success();
    }
}