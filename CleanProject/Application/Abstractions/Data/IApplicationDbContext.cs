using Domain.Features.BlogPosts;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.Data;

/// <summary>
/// Configures database entities.
/// </summary>
public interface IApplicationDbContext
{
    /// <inheritdoc cref="BlogPost"/>
    DbSet<BlogPost> BlogPosts { get; set; }
}