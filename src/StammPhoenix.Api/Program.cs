using FastEndpoints;
using FastEndpoints.ClientGen.Kiota;
using FastEndpoints.Swagger;
using Kiota.Builder;
using Serilog;
using StammPhoenix.Application;
using StammPhoenix.Application.Interfaces;
using StammPhoenix.Infrastructure;

namespace StammPhoenix.Api;

public static class Program
{
   public static readonly DateTime StartupTime = DateTime.UtcNow;

   public static async Task Main(string[] args)
   {
      var builder = WebApplication.CreateBuilder();

      builder.Services
         .AddApplicationServices()
         .AddInfrastructureServices()
         .AddApiServices();

      var app = builder.Build();

      var appConfiguration = app.Services.GetRequiredService<IAppConfiguration>();

      Log.Logger = new LoggerConfiguration()
         .WriteTo.Console()
         .WriteTo.File(Path.Combine(appConfiguration.LogPath, "log-.txt"), rollingInterval: RollingInterval.Day)
         .CreateLogger();

      app.UseSerilogRequestLogging();

      app.UseAuthentication()
         .UseAuthorization()
         .UseFastEndpoints(c =>
         {
            c.Endpoints.Configurator = ep =>
            {
               ep.DontAutoTag();
            };
         })
         .UseSwaggerGen();

      if (app.Configuration["generateclients"] == "true")
      {
         if (Directory.Exists("../StammPhoenix.Web/src/lib/api"))
         {
            Directory.Delete("../StammPhoenix.Web/src/lib/api", true);
         }

         Directory.CreateDirectory("../StammPhoenix.Web/src/lib/api");

         await app.GenerateApiClientsAndExitAsync(
            c =>
            {
               c.SwaggerDocumentName = "current";
               c.Language = GenerationLanguage.TypeScript;
               c.OutputPath = "../StammPhoenix.Web/src/lib/api";
               c.ClientNamespaceName = "StammPhoenix";
               c.ClientClassName = "ApiClient";
            });
      }

      await app.RunAsync();
   }
}
