using Domain.Primitives;

namespace Domain.Abstractions;

/// <summary>
/// Defines common operations for all repositories.
/// </summary>
/// <typeparam name="TEntity">The type used by the repository.</typeparam>
public interface IGenericRepository<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Retrieves an entity from the database if one exists.
    /// </summary>
    /// <param name="id">Represents the unique identifier of an entity.</param>
    /// <returns>Entity or null if it doesn't exist in the database.</returns>
    Task<TEntity?> GetById(int id);

    /// <summary>
    /// Retrieves all entities from the database.
    /// </summary>
    /// <returns>List of all entities.</returns>
    Task<IReadOnlyList<TEntity>> GetAll();
    
    /// <summary>
    /// Inserts entity into the database.
    /// </summary>
    /// <param name="entity">Entity to be added into the database.</param>
    void Add(TEntity entity);
    
    /// <summary>
    /// Updates entity in the database.
    /// </summary>
    /// <param name="entity">Entity to be updated in the database.</param>
    void Update(TEntity entity);
    
    /// <summary>
    /// Deletes an entity in the database.
    /// </summary>
    /// <param name="entity">Entity to be deleted in the database.</param>
    void Delete(TEntity entity);
}