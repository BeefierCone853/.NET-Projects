using Application.DTOs.Persons;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Features.Persons.Queries.GetPersonList;

public class GetPersonListRequestHandler(
    IPersonRepository personRepository,
    IMapperBase mapper)
    : IRequestHandler<GetPersonListRequest, List<PersonDto>>
{
    public async Task<List<PersonDto>> Handle(GetPersonListRequest request, CancellationToken cancellationToken)
    {
        var persons = await personRepository.GetAll();
        return mapper.Map<List<PersonDto>>(persons);
    }
}