using Application.DTOs.Persons;
using MediatR;

namespace Application.Features.Persons.Queries.GetPersonList;

public class GetPersonListRequest : IRequest<List<PersonDto>>;