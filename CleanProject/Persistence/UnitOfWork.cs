using Domain.Repositories;

namespace Persistence;

/// <inheritdoc cref="IUnitOfWork"/>
/// <param name="dbContext">Represents a session with the database.</param>
internal sealed class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    /// <summary>
    /// Saves changes made to the database.
    /// </summary>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    /// <returns>Number of changes written to the database.</returns>
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}