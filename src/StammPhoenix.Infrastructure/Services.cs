using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using StammPhoenix.Infrastructure.Persistence;
using StammPhoenix.Infrastructure.Persistence.Interceptors;

namespace StammPhoenix.Infrastructure;

public static class Services
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<DatabaseContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseNpgsql()
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<DatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        return services;
    }
}
