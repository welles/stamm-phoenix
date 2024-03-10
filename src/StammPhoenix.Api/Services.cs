using FastEndpoints;
using FastEndpoints.Swagger;
using NJsonSchema.Generation;
using Serilog;
using StammPhoenix.Api.Core;
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

                d.TagDescriptions = t =>
                {
                    t[MetaGroup.GroupName] = "Endpoints concerning metadata about the API";
                };
            })
            .AddSerilog();

        services.AddScoped<IUser, CurrentUser>();

        return services;
    }
}
