using Domain.Abstractions;

namespace Domain.Features.BlogPosts;

/// <summary>
/// Repository for performing database operations on <see cref="BlogPost"/> entity.
/// </summary>
public interface IBlogPostRepository : IGenericRepository<BlogPost>
{
    /// <summary>
    /// Checks if an entity exists in the database.
    /// </summary>
    /// <param name="id">Represents the unique identifier of an entity.</param>
    /// <returns>Boolean if an entity exists.</returns>
    Task<bool> Exists(int id);
    
    /// <summary>
    /// Gets queryable interface for <see cref="BlogPost"/>s.
    /// </summary>
    /// <returns><see cref="BlogPost"/>Queryable.</returns>
    IQueryable<BlogPost> GetQueryable();
}