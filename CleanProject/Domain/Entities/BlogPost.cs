using Domain.Primitives;

namespace Domain.Entities;

public sealed class BlogPost : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }
}