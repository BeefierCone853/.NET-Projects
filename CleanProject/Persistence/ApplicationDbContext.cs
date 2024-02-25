using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

/// <summary>
/// Configures database options and provides a database session.
/// </summary>
/// <param name="options">Database options.</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    /// <inheritdoc cref="BlogPost"/>
    public DbSet<BlogPost> BlogPosts { get; set; }
}