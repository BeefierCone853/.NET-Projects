namespace Domain.Primitives;

/// <summary>
/// Base type for all database entities.
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// Unique identifier of an entity.
    /// </summary>
    public int Id { get; set; }
}