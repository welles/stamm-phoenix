using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Infrastructure.Core;
using StammPhoenix.Infrastructure.Persistence;
using StammPhoenix.Infrastructure.Persistence.Interceptors;

namespace StammPhoenix.Infrastructure;

public static class Services
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<DatabaseContext>();

        services.AddScoped<IDatabaseManager>(provider => provider.GetRequiredService<DatabaseContext>());
        services.AddScoped<ILeaderRepository>(provider => provider.GetRequiredService<DatabaseContext>());
        services.AddScoped<IEventRepository>(provider => provider.GetRequiredService<DatabaseContext>());

        services.AddTransient<IPasswordHasher, BCryptPasswordHasher>();

        return services;
    }
}
