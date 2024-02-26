namespace Application.Abstractions.Data;

/// <summary>
/// Used to group multiple database-related operations into a single transaction.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all current database changes.
    /// </summary>
    /// <param name="cancellationToken">Signals if a task or operation should be cancelled.</param>
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}