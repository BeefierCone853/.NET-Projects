using Application.Abstractions.Data;
using Domain.Features.BlogPosts;
using Domain.Shared;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

/// <summary>
/// Used for injecting this project as a service.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Configures dependencies for the project and injects it as a service. 
    /// </summary>
    /// <param name="services">Service descriptor for this project.</param>
    /// <param name="configuration">Application configuration properties.</param>
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgreSQL");
        var identityConnectionString = configuration.GetConnectionString("PostgreSQL.Identity");
        Ensure.NotNullOrEmpty(connectionString);
        services.AddTransient<IDbConnectionFactory>(_ => new DbConnectionFactory(connectionString));
        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddDbContext<IdentityDbContext, IdentityDataContext>(options =>
            options.UseNpgsql(identityConnectionString));
        services.AddAuthorization();
        services.AddIdentityApiEndpoints<IdentityUser>()
            .AddEntityFrameworkStores<IdentityDataContext>();
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
}