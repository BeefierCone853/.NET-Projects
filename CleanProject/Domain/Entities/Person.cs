using Domain.Primitives;

namespace Domain.Entities;

public sealed class Person : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string CollegeName { get; set; }
}