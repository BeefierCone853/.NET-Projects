using Domain.Abstractions;
using Domain.Primitives;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Generic;

/// <inheritdoc cref="IGenericRepository{TEntity}"/>
/// <param name="dbContext">Represents a session with the database.</param>
internal abstract class GenericRepository<TEntity>(ApplicationDbContext dbContext) : IGenericRepository<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Represents a session with the database.
    /// </summary>
    protected readonly ApplicationDbContext DbContext = dbContext;

    public virtual async Task<TEntity?> GetById(int id)
    {
        return await DbContext.Set<TEntity>().FindAsync(id);
    }

    public async Task<IReadOnlyList<TEntity>> GetAll()
    {
        return await DbContext.Set<TEntity>().ToListAsync();
    }

    public void Add(TEntity entity)
    {
        DbContext.Set<TEntity>().Add(entity);
    }

    public void Update(TEntity entity)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public void Delete(TEntity entity)
    {
        DbContext.Set<TEntity>().Remove(entity);
    }
}