using System.Data;
using Application.Abstractions.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data;

/// <inheritdoc cref="IDbConnectionFactory"/>
/// <param name="connectionString">String with database connection information.</param>
public class DbConnectionFactory(string connectionString) : IDbConnectionFactory
{
    public IDbConnection CreateOpenConnection()
    {
        var connection = new SqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}