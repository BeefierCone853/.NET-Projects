using Domain.Primitives;

namespace Domain.Entities;

public sealed class Person(int id, string firstName, string lastName, string collegeName) : Entity(id)
{
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public string CollegeName { get; private set; } = collegeName;
}