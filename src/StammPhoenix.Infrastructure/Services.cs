using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StammPhoenix.Infrastructure.Persistence;

namespace StammPhoenix.Infrastructure;

public static class Services
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>((sp, options) =>
        {
            options.UseNpgsql()
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<DatabaseContext>(provider => provider.GetRequiredService<DatabaseContext>());

        return services;
    }
}
