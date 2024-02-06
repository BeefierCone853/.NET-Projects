namespace Domain.Primitives;

public abstract class Entity(int id)
{
    public int Id { get; protected set; } = id;
}