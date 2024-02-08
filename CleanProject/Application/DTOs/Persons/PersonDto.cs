using Domain.Primitives;

namespace Application.DTOs.Persons;

public record PersonDto(string FirstName, string LastName, string CollegeName, int Id) : BaseDto(Id);