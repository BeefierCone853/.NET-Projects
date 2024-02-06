using Domain.Primitives;

namespace Application.DTOs.Persons;

public sealed record PersonDto(string FirstName, string LastName, string CollegeName, int Id) : BaseDto(Id);