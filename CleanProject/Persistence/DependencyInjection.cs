using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;

namespace Persistence;

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
    /// <returns>Configured services.</returns>
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("PostgreSQL")));
        services.AddScoped<IBlogPostRepository, BlogPostRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}