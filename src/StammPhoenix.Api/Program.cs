using FastEndpoints;
using FastEndpoints.Swagger;
using NJsonSchema.Generation;
using Serilog;
using StammPhoenix.Api.Endpoints.MetaGroup;
using Environment = StammPhoenix.Api.Core.Environment;

namespace StammPhoenix.Api;

public static class Program
{
   public static readonly DateTime StartupTime = DateTime.UtcNow;

   public static void Main(string[] args)
   {
      var environment = Environment.GetVariables();

      Log.Logger = new LoggerConfiguration()
         .WriteTo.Console()
         .WriteTo.File(Path.Combine(environment.LogPath, "log-.txt"), rollingInterval: RollingInterval.Day)
         .CreateLogger();

      var builder = WebApplication.CreateBuilder();
      builder.Services
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

      var app = builder.Build();

      app.UseSerilogRequestLogging();

      app.UseFastEndpoints(c =>
         {
            c.Endpoints.Configurator = ep =>
            {
               ep.DontAutoTag();
            };
         })
         .UseSwaggerGen();

      app.Run();
   }
}
