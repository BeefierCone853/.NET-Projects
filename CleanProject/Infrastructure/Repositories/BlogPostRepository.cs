using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories.Generic;

namespace Infrastructure.Repositories;

/// <inheritdoc cref="IBlogPostRepository"/>
/// <param name="dbContext">Represents a session with the database.</param>
internal class BlogPostRepository(ApplicationDbContext dbContext)
    : GenericRepository<BlogPost>(dbContext), IBlogPostRepository
{
    /// <summary>
    /// Checks if entity exists in the database.
    /// </summary>
    /// <param name="id">Unique identifier of an entity.</param>
    /// <returns>True or false if the entity exists.</returns>
    public async Task<bool> Exists(int id)
    {
        var entity = await GetById(id);
        return entity != null;
    }

    public IQueryable<BlogPost> GetQueryable()
    {
        return DbContext.BlogPosts.AsQueryable();
    }
}