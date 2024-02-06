using Application.DTOs;
using Application.DTOs.Persons;
using MediatR;

namespace Application.Features.Persons.Queries.GetPersonDetail;

public class GetPersonDetailRequest : IRequest<PersonDto>
{
    public int Id { get; set; }
}