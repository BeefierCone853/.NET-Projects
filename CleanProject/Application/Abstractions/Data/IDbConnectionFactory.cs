using System.Data;

namespace Application.Abstractions.Data;

/// <summary>
/// Factory for creating database connections.
/// </summary>
public interface IDbConnectionFactory
{
    /// <summary>
    /// Creates a database connection.
    /// </summary>
    /// <returns>Database connection.</returns>
    IDbConnection CreateOpenConnection();
}