using Application.Abstractions.Behaviors;
using Application.Profiles;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(assembly);
                configuration.AddOpenBehavior(typeof(RequestLoggingPipelineBehavior<,>));
                configuration.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            }
        );
        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);
        services.AddAutoMapper(typeof(MappingProfile));
        return services;
    }
}