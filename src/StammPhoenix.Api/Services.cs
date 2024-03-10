using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using NJsonSchema.Generation;
using Serilog;
using StammPhoenix.Api.Core;
using StammPhoenix.Api.Endpoints.Auth;
using StammPhoenix.Api.Endpoints.MetaGroup;
using StammPhoenix.Application.Interfaces;

namespace StammPhoenix.Api;

public static class Services
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
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

                d.TagDescriptions = tag =>
                {
                    tag[MetaGroup.GroupName] = "Endpoints concerning metadata about the API";
                    tag[AuthGroup.GroupName] = "Endpoints for authorizing with the API";
                };
            })
            .AddSerilog()
            .AddAuthenticationJwtBearer(_ => {})
            .AddAuthorization();

        services.AddScoped<IUser, CurrentUser>();

        services.AddSingleton<IAppConfiguration, EnvironmentAppConfiguration>();

        return services;
    }
}
