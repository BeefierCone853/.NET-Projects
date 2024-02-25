using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence;

/// <summary>
/// Factory for the database configuration.
/// </summary>
public class CleanProjectManagementDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    /// <summary>
    /// Configures the applications database context.
    /// </summary>
    /// <param name="args">Array of arguments.</param>
    /// <returns>Configured database context.</returns>
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        builder.UseNpgsql(connectionString);
        return new ApplicationDbContext(builder.Options);
    }
}