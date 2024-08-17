using System.Reflection;
using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using NJsonSchema.Generation;
using Serilog;
using StammPhoenix.Api.Core;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Infrastructure.Configuration;

namespace StammPhoenix.Api;

public static class Services
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        var environment = new EnvironmentAppConfiguration();

        services.AddSingleton<IAppConfiguration>(environment);

        services.AddSingleton<IDatabaseConfiguration, EnvironmentDatabaseConfiguration>();

        services.AddSingleton<IMapper, Mapper>();

        services
            .AddFastEndpoints()
            .SwaggerDocument(d =>
            {
                d.DocumentSettings = s =>
                {
                    s.Title = "Stamm Phoenix API";
                    s.DocumentName = "current";
                    s.Version = "Current";
                    s.SchemaSettings.SchemaNameGenerator = new DefaultSchemaNameGenerator();
                };

                var endpointGroups = Assembly.GetAssembly(typeof(Services))!
                    .GetTypes()
                    .Where(x => x.IsSubclassOf(typeof(EndpointGroup)))
                    .Select(g => Activator.CreateInstance(g) as EndpointGroup)
                    .OfType<EndpointGroup>()
                    .ToArray();

                d.TagDescriptions = tags =>
                {
                    foreach (var endpointGroup in endpointGroups)
                    {
                        tags[endpointGroup.GroupName] = endpointGroup.Description;
                    }
                };
            })
            .AddSerilog()
            .AddAuthenticationJwtBearer(o =>
            {
                o.SigningStyle = TokenSigningStyle.Asymmetric;
                o.SigningKey = environment.PublicSigningKey;
                o.KeyIsPemEncoded = true;
            })
            .AddAuthorization();

        services.AddScoped<ICurrentUser, ApiCurrentUser>();

        services.AddCors(options =>
        {
            options.AddPolicy(name: EnvironmentNames.ALLOWED_HOSTS,
                policy  =>
                {
                    policy.WithOrigins(environment.AllowedHosts)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        return services;
    }
}
