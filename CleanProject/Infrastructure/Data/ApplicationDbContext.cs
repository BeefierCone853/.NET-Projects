using Application.Abstractions.Data;
using Domain.Features.BlogPosts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <inheritdoc cref="IApplicationDbContext"/>
/// <param name="options">Database options.</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<BlogPost> BlogPosts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}