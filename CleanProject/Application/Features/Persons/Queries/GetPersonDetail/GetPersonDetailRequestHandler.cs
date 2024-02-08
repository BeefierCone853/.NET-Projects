using Application.DTOs.Persons;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Queries.GetPersonDetail;

public class GetPersonDetailRequestHandler(
    IPersonRepository personRepository,
    IMapper mapper) : IRequestHandler<GetPersonDetailRequest, PersonDto>
{
    public async Task<PersonDto> Handle(GetPersonDetailRequest request, CancellationToken cancellationToken)
    {
        var person = await personRepository.Get(request.Id);
        return mapper.Map<PersonDto>(person);
    }
}